import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Room } from './room';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  private roomUri: string = 'api/room';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getRooms(): Observable<Room[]> {
    return this.http.get<Room[]>(this.roomUri);
  }

  getRoom(id: number): Observable<Room> {
    const url = `${this.roomUri}/${id}`;
    return this.http.get<Room>(url);
  }
}
