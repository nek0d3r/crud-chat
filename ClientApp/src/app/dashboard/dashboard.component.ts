import { Component, OnInit } from '@angular/core';

import { SphereService } from '../sphere.service';

import { Sphere } from '../sphere';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  private spheres: Sphere[];

  constructor(private sphereService: SphereService) { }

  getSpheres(): void {
    this.sphereService.getSpheres().subscribe(_ => this.spheres = _);
  }

  ngOnInit() {
    this.getSpheres();
  }

}
