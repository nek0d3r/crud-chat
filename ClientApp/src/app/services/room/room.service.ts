import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';

import { Room } from '@app/models/room/room';
import { Message } from '@app/models/message/message';

import { MatSnackBar } from '@angular/material';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  private roomUri: string = 'api/room';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    body: null
  };

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  getAllRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(this.roomUri, this.httpOptions).pipe(
      catchError(this.handleError<Room[]>('There was a problem getting rooms', undefined))
    );
  }

  getRoom(id: number): Observable<Room> {
    const url = `${this.roomUri}/${id}`;
    return this.http.get<Room>(url, this.httpOptions).pipe(
      catchError(this.handleError<Room>('There was a problem getting the room', undefined))
    );
  }

  getRoomMessages(id: number): Observable<Message[]> {
    const url = `${this.roomUri}/${id}/messages`;
    return this.http.get<Message[]>(url, this.httpOptions).pipe(
      catchError(this.handleError<Message[]>('There was a problem getting messages', undefined))
    );
  }

  postRoomMessage(id: number, message: Message): Observable<Message> {
    const url = `${this.roomUri}/${id}/messages`;
    return this.http.post<Message>(url, message, this.httpOptions).pipe(
      catchError(this.handleError<Message>('There was a problem adding the message', undefined))
    );
  }

  putRoom(id: number, room: Room): Observable<Room> {
    const url = `${this.roomUri}/${id}`;
    return this.http.put<Room>(url, room, this.httpOptions).pipe(
      catchError(this.handleError<Room>('There was a problem updating the room', undefined))
    );
  }

  deleteRoom(id: number): Observable<Room> {
    const url = `${this.roomUri}/${id}`;
    return this.http.delete<Room>(url, this.httpOptions).pipe(
      catchError(this.handleError<Room>('There was a problem deleting the room', undefined))
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
