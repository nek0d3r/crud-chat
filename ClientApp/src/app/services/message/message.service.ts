import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { MatSnackBar } from '@angular/material';

import { Message } from '@app/models/message/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private messageUri: string = "api/message";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  getAllMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(this.messageUri, this.httpOptions).pipe(
      catchError(this.handleError<Message[]>('There was a problem getting messages', undefined))
    );
  }

  getMessage(id: number): Observable<Message> {
    const url = `${this.messageUri}/${id}`;
    return this.http.get<Message>(url, this.httpOptions).pipe(
      catchError(this.handleError<Message>('There was a problem getting messages', undefined))
    );
  }

  putMessage(id: number, message: Message): Observable<Message> {
    const url = `${this.messageUri}/${id}`;
    return this.http.put<Message>(url, message, this.httpOptions).pipe(
      catchError(this.handleError<Message>('There was a problem saving the message', undefined))
    );
  }

  deleteMessage(id: number): Observable<Message> {
    const url = `${this.messageUri}/${id}`;
    return this.http.delete<Message>(url, this.httpOptions).pipe(
      catchError(this.handleError<Message>('There was a problem deleting the message', undefined))
    );
  }

  handleError<T>(message: string, result?: T): any {
      return (error: any): Observable<T> => {
        this.snackBar.open(message, "Dismiss", { duration: 2000 });
  
        // Let the app keep running by returning an empty result
        return of(result as T);
      }
  }
}
