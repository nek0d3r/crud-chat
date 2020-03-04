import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Message } from '@app/models/message/message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private messageUri: string = "api/message";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getAllMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(this.messageUri, this.httpOptions);
  }

  getMessage(id: number): Observable<Message> {
    const url = `${this.messageUri}/${id}`;
    return this.http.get<Message>(url, this.httpOptions);
  }

  putMessage(id: number, message: Message): Observable<Message> {
    const url = `${this.messageUri}/${id}`;
    return this.http.put<Message>(url, message, this.httpOptions);
  }

  deleteMessage(id: number): Observable<Message> {
    const url = `${this.messageUri}/${id}`;
    return this.http.delete<Message>(url, this.httpOptions);
  }
}