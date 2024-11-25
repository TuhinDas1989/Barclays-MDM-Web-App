import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Task } from '../models/task.model';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  private apiUrl = 'http://localhost:5175/api/task';

  constructor(private http: HttpClient) {}

  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl).pipe(
        catchError(error => {
            console.error('An error occurred:', error);
            alert(error.error);
            return throwError('Something went wrong');
        })
      );
  }

  addTask(task: Task): Observable<Task> {
    return this.http.post<Task>(this.apiUrl, task).pipe(
        catchError(error => {
            console.error('An error occurred:', error);
            alert(error.error);
            return throwError('Something went wrong');
        })
      );
  }

  editTask(task: Task): Observable<Task> {
    return this.http.put<Task>(this.apiUrl, task).pipe(
        catchError(error => {
            console.error('An error occurred:', error);
            alert(error.error);
            return throwError('Something went wrong');
        })
      );
  }

  deleteTask(task: Task): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${task.id}`).pipe(
        catchError(error => {
            console.error('An error occurred:', error);
            alert(error.error);
            return throwError('Something went wrong');
        })
      );
  }
}