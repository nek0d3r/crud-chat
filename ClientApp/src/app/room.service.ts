import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpRequest } from '@angular/common/http';

import { Room } from './room';
import { Message } from './message';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  private roomUri: string = 'api/room';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    body: null
  };

  constructor(private http: HttpClient) { }

  getAllRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(this.roomUri, this.httpOptions);
  }

  getRoom(id: number): Observable<Room> {
    const url = `${this.roomUri}/${id}`;
    return this.http.get<Room>(url, this.httpOptions);
  }

  getRoomMessages(id: number): Observable<Message[]> {
    const url = `${this.roomUri}/${id}/messages`;
    return this.http.get<Message[]>(url, this.httpOptions);
  }

  postRoomMessage(id: number, message: Message): Observable<Message> {
    const url = `${this.roomUri}/${id}/messages`;
    return this.http.post<Message>(url, message, this.httpOptions);
  }

  putRoom(id: number, room: Room): Observable<Room> {
    const url = `${this.roomUri}/${id}`;
    return this.http.put<Room>(url, room, this.httpOptions);
  }

  deleteRoom(id: number): void {
    const url = `${this.roomUri}/${id}`;
    this.http.delete(url, this.httpOptions);
  }
}
