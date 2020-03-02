import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';

import { SphereService } from '../sphere.service';

import { Sphere } from '../sphere';

import { SphereDialogComponent } from '../sphere-dialog/sphere-dialog.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  private spheres: Sphere[];

  constructor(
    private dialog: MatDialog,
    private sphereService: SphereService) { }

  getSpheres(): void {
    this.sphereService.getAllSpheres().subscribe(_ => this.spheres = _);
  }

  addSphere(name: string, desc: string): void {
    const sphere: Sphere = { sphereId: 0, name: name, dateCreated: new Date(), rooms: [] };
    this.sphereService.postSphere(sphere).subscribe(_ => this.spheres.push(_));
  }

  ngOnInit() {
    this.getSpheres();
  }

  newSphereModal(): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.width = '500px';

    const dialogRef = this.dialog.open(SphereDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => this.addSphere(data.name, data.desc)
    );
  }

}
