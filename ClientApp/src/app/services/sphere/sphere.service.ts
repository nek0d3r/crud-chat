import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Sphere } from '@app/models/sphere/sphere';
import { Room } from '@app/models/room/room';

import { MatSnackBar } from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class SphereService {

  private sphereUri: string = 'api/sphere';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient, private snackBar: MatSnackBar) { }

  getAllSpheres(): Observable<Sphere[]> {
    return this.http.get<Sphere[]>(this.sphereUri, this.httpOptions).pipe(
      catchError(this.handleError<Sphere[]>('There was a problem getting spheres', []))
    );
  }

  getSphere(id: number): Observable<Sphere> {
    const url = `${this.sphereUri}/${id}`;
    return this.http.get<Sphere>(url, this.httpOptions).pipe(
      catchError(this.handleError<Sphere>('There was a problem getting the sphere', null))
    );
  }

  getSphereRooms(id: number): Observable<Room[]> {
    const url = `${this.sphereUri}/${id}/rooms`;
    return this.http.get<Room[]>(url, this.httpOptions).pipe(
      catchError(this.handleError<Room[]>('There was a problem getting rooms', []))
    );
  }

  postSphere(sphere: Sphere): Observable<Sphere> {
    return this.http.post<Sphere>(this.sphereUri, sphere, this.httpOptions).pipe(
      catchError(this.handleError<Sphere>('There was a problem adding the sphere', null))
    );
  }

  postSphereRoom(id: number, room: Room): Observable<Room> {
    const url = `${this.sphereUri}/${id}/rooms`;
    return this.http.post<Room>(url, room, this.httpOptions).pipe(
      catchError(this.handleError<Room>('There was a problem adding the room', null))
    );
  }

  putSphere(id: number, sphere: Sphere): Observable<Sphere> {
    const url = `${this.sphereUri}/${id}`;
    return this.http.put<Sphere>(url, sphere, this.httpOptions).pipe(
      catchError(this.handleError<Sphere>('There was a problem updating the sphere', null))
    );
  }

  deleteSphere(id: number): Observable<Sphere> {
    const url = `${this.sphereUri}/${id}`;
    return this.http.delete<Sphere>(url, this.httpOptions).pipe(
      catchError(this.handleError<Sphere>('There was a problem deleting the sphere', null))
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
