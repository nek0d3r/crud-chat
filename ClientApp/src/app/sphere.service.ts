import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Sphere } from './sphere';

@Injectable({
  providedIn: 'root'
})
export class SphereService {

  private sphereUri: string = 'api/sphere';

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  constructor(private http: HttpClient) { }

  getSpheres(): Observable<Sphere[]> {
    return this.http.get<Sphere[]>(this.sphereUri);
  }

  getSphere(id: number): Observable<Sphere> {
    const url = `${this.sphereUri}/${id}`;
    return this.http.get<Sphere>(url);
  }
}
