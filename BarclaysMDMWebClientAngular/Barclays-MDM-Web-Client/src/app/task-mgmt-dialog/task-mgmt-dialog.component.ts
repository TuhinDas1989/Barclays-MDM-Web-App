import { Component, Inject, inject, Input, model, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Task, TaskStatus } from '../../models/task.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-task-mgmt-dialog',
  standalone: false,
  templateUrl: './task-mgmt-dialog.component.html',
  styleUrl: './task-mgmt-dialog.component.scss'
})
export class TaskMgmtDialogComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<TaskMgmtDialogComponent>);
  headerText: string;
  form: FormGroup;

  taskData: Task;

  statusList: TaskStatus[] = [
    {value: 'Not Started', text: 'Not Started'},
    {value: 'In Progress', text: 'In Progress'},
    {value: 'Completed', text: 'Completed'}
  ];

  constructor(@Inject(FormBuilder) public formBuilder: FormBuilder, @Inject(MAT_DIALOG_DATA) data: any) {
    this.taskData = data.task;
    this.form = new FormGroup ({
      taskId: new FormControl('', Validators.required),
      taskName: new FormControl('', Validators.required),
      taskPriority: new FormControl('', [Validators.required, Validators.min(1), Validators.max(10)]),
      taskStatus: new FormControl('', Validators.required)
    });
    this.headerText = this.taskData.id == 0 ? 'Add New Task' : 'Modify Task';
  }
  ngOnInit() {
    this.form.patchValue({
      taskId: this.taskData.id,
      taskName: this.taskData.name,
      taskPriority: this.taskData.priority,
      taskStatus: this.taskData.status
    });
  }

  save() {
    this.dialogRef.close(this.form.value);
  }

  close() {
    this.dialogRef.close();
  }
}
