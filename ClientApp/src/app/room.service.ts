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
    return this.http.get<Room[]>(this.roomUri);
  }

  getRoom(id: number): Observable<Room> {
    const url = `${this.roomUri}/${id}`;
    return this.http.get<Room>(url);
  }

  getRoomMessages(id: number): Observable<Message[]> {
    const url = `${this.roomUri}/${id}/messages`;
    return this.http.get<Message[]>(url);
  }
}
