import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';

import { SphereService } from '../sphere.service';

import { Sphere } from '../sphere';

import { SphereDialogComponent } from '../sphere-dialog/sphere-dialog.component';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

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
    const sphere: Sphere = { sphereId: 0, name: name, description: desc, dateCreated: new Date() };
    this.sphereService.postSphere(sphere).subscribe(_ => this.spheres.push(_));
  }

  changeSphere(id: number, name: string, desc: string): void {
    const sphere: Sphere = this.spheres.find(_ => _.sphereId === id);
    sphere.name = name;
    sphere.description = desc;
    this.sphereService.putSphere(id, sphere).subscribe(_ => {
      this.spheres.splice(this.spheres.indexOf(this.spheres.find(s => s.sphereId === _.sphereId)), 1, _);
    });
  }

  deleteSphere(id: number): void {
    this.sphereService.deleteSphere(id).subscribe(_ => {
      this.spheres.splice(this.spheres.indexOf(this.spheres.find(s => s.sphereId === id)), 1);
    });
  }

  ngOnInit() {
    this.getSpheres();
  }

  newSphereModal(): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.width = '500px';
    dialogConfig.data = { dialogTitle: "New Sphere", name: "", description: "" };

    const dialogRef = this.dialog.open(SphereDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => { if(data != undefined) this.addSphere(data.name, data.description) }
    );
  }

  editSphereModal(sphere: Sphere): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.width = '500px';
    dialogConfig.data = { dialogTitle: "Edit Sphere", name: sphere.name, description: sphere.description };

    const dialogRef = this.dialog.open(SphereDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(data => {
      if(data != undefined &&
        (data.name !== sphere.name || data.description !== sphere.description))
      {
        this.changeSphere(sphere.sphereId, data.name, data.description);
      }
    });
  }

  deleteSphereModal(sphere: Sphere): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.width = '500px';
    dialogConfig.data = {
      title: 'Remove Sphere',
      details: 'Are you sure you want to remove this sphere? All rooms and messages will be removed as well. This action cannot be undone.',
      action: 'Delete',
      color: 'mat-warn'
    }

    const dialogRef = this.dialog.open(ConfirmDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(data => {
      if(data != undefined &&
        data.response === true)
      {
        this.deleteSphere(sphere.sphereId);
      }
    });
  }

}
