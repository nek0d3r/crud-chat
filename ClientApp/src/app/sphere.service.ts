import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Sphere } from './sphere';
import { Room } from './room';

@Injectable({
  providedIn: 'root'
})
export class SphereService {

  private sphereUri: string = 'api/sphere';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getAllSpheres(): Observable<Sphere[]> {
    return this.http.get<Sphere[]>(this.sphereUri, this.httpOptions);
  }

  getSphere(id: number): Observable<Sphere> {
    const url = `${this.sphereUri}/${id}`;
    return this.http.get<Sphere>(url, this.httpOptions);
  }

  getSphereRooms(id: number): Observable<Room[]> {
    const url = `${this.sphereUri}/${id}/rooms`;
    return this.http.get<Room[]>(url, this.httpOptions);
  }

  postSphere(sphere: Sphere): Observable<Sphere> {
    return this.http.post<Sphere>(this.sphereUri, sphere, this.httpOptions);
  }

  postSphereRoom(id: number, room: Room): Observable<Room> {
    const url = `${this.sphereUri}/${id}/rooms`;
    return this.http.post<Room>(url, room, this.httpOptions);
  }

  putSphere(id: number, sphere: Sphere): Observable<Sphere> {
    const url = `${this.sphereUri}/${id}`;
    return this.http.put<Sphere>(url, sphere, this.httpOptions);
  }

  deleteSphere(id: number): Observable<Sphere> {
    const url = `${this.sphereUri}/${id}`;
    return this.http.delete<Sphere>(url, this.httpOptions);
  }
}
