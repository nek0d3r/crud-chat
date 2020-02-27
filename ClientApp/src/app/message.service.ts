import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Message } from './message';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private messageUri: string = "api/message";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getMessages(): Observable<Message[]> {
    return this.http.get<Message[]>(this.messageUri);
  }

  getMessage(id: number): Observable<Message> {
    const url = `${this.messageUri}/${id}`;
    return this.http.get<Message>(url);
  }
}
