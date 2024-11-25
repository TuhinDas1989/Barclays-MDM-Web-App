import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskMgmtComponent } from './task-mgmt.component';

describe('TaskMgmtComponent', () => {
  let component: TaskMgmtComponent;
  let fixture: ComponentFixture<TaskMgmtComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TaskMgmtComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TaskMgmtComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
