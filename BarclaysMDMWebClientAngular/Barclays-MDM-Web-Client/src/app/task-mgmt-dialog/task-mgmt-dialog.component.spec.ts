import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskMgmtDialogComponent } from './task-mgmt-dialog.component';

describe('TaskMgmtDialogComponent', () => {
  let component: TaskMgmtDialogComponent;
  let fixture: ComponentFixture<TaskMgmtDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TaskMgmtDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskMgmtDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
