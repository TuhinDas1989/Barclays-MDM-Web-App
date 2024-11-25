import { ChangeDetectionStrategy, Component, ElementRef, inject, OnInit, ViewChild } from '@angular/core';
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task.model';
import { MatDialog } from '@angular/material/dialog';
import { TaskMgmtDialogComponent } from '../task-mgmt-dialog/task-mgmt-dialog.component';

@Component({
  selector: 'app-task-mgmt',
  standalone: false,
  templateUrl: './task-mgmt.component.html',
  styleUrl: './task-mgmt.component.scss'
})
export class TaskMgmtComponent implements OnInit {
  tasks: Task[] = [];
  newTask: Task = { id: 0, name: '', priority: 0, status: 'Not Started' };
  displayedColumns: string[] = ['name', 'priority', 'status', 'action'];
  readonly dialog = inject(MatDialog);

  constructor(private taskService: TaskService) {}

  ngOnInit() {
    this.loadTasks();
  }

  loadTasks() {
    this.taskService.getTasks().subscribe((tasks) => {
      this.tasks = tasks;
    });
  }

  addTask() {
    this.newTask = { id: 0, name: '', priority: 0, status: 'Not Started' };

    const dialogRef = this.dialog.open(TaskMgmtDialogComponent, {
      data: { task: this.newTask },
    });

    dialogRef.afterClosed().subscribe(result => {
        if (result !== undefined) {
          this.newTask = { id: result.taskId, name: result.taskName, priority: result.taskPriority, status: result.taskStatus };
          // console.log(this.newTask);
          this.taskService.addTask(this.newTask).subscribe(() => {
            this.loadTasks();
            this.newTask = { id: 0, name: '', priority: 0, status: 'Not Started' };
          });
        }
      });
  }

  editTask(task: Task) {
    const dialogRef = this.dialog.open(TaskMgmtDialogComponent, {
      data: { task: task },
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        const editTask: Task = { id: result.taskId, name: result.taskName, priority: result.taskPriority, status: result.taskStatus };
        // console.log(editTask);
        this.taskService.editTask(editTask).subscribe(() => {
          this.loadTasks();
        });
      }
    });
  }

  deleteTask(task: Task) {
    var result = confirm('Do you want to delete the Task?');
    if (result) {
      this.taskService.deleteTask(task).subscribe(() => {
        this.loadTasks();
      });
    }
  }
}
