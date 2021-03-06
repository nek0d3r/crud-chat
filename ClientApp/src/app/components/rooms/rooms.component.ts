import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog, MatDialogConfig, MatSnackBar } from '@angular/material';

import { SphereService } from '@app/services/sphere/sphere.service';
import { RoomService } from '@app/services/room/room.service';

import { Sphere } from '@app/models/sphere/sphere';
import { Room } from '@app/models/room/room';

import { RoomDialogComponent } from '@app/components/room-dialog/room-dialog.component';
import { ConfirmDialogComponent } from '@app/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {

  private sphereId: number;
  private sphere: Sphere;
  
  private rooms: Room[];

  constructor(
    private dialog: MatDialog,
    private sphereService: SphereService,
    private roomService: RoomService,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar) {}

  getRooms(): void {
    this.sphereService.getSphereRooms(this.sphereId).subscribe(_ => { if(_ !== undefined) { this.rooms = _ } })
  }

  addRoom(title: string): void {
    const room: Room = { roomId: 0, title: title, dateCreated: new Date() };
    this.sphereService.postSphereRoom(this.sphereId, room).subscribe(_ => {
      if(_ !== undefined) {
        this.rooms.push(_);
        this.snackBar.open('Successfully added room', 'Dismiss', { duration: 2000 });
      }
    });
  }

  changeRoom(id: number, title: string): void {
    const room: Room = this.rooms.find(_ => _.roomId === id);
    room.title = title;
    this.roomService.putRoom(id, room).subscribe(_ => {
      if(_ !== undefined) {
        this.rooms.splice(this.rooms.indexOf(this.rooms.find(r => r.roomId === _.roomId)), 1, _);
        this.snackBar.open('Successfully changed room', 'Dismiss', { duration: 2000 });
      }
    });
  }

  deleteRoom(id: number): void {
    this.roomService.deleteRoom(id).subscribe(_ => {
      if(_ !== undefined)
      {
        this.rooms.splice(this.rooms.indexOf(this.rooms.find(r => r.roomId === id)), 1);
        this.snackBar.open('Successfully deleted room', 'Dismiss', { duration: 2000 });
      }
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(_ => {
      this.sphereId = parseInt(_.get('id'));
    });
    this.sphereService.getSphere(this.sphereId).subscribe(_ => this.sphere = _);
    this.getRooms();
  }

  newRoomModal(): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.width = '500px';
    dialogConfig.data = { dialogTitle: "New Room", title: "" };

    const dialogRef = this.dialog.open(RoomDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      data => { if(data != undefined) this.addRoom(data.title) }
    );
  }

  editRoomModal(room: Room): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.width = '500px';
    dialogConfig.data = { dialogTitle: "Edit Room", title: room.title };

    const dialogRef = this.dialog.open(RoomDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(data => {
      if(data != undefined &&
        data.title !== room.title)
      {
        this.changeRoom(room.roomId, data.title);
      }
    });
  }

  deleteRoomModal(room: Room): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.width = '500px';
    dialogConfig.data = {
      title: 'Remove Room',
      details: 'Are you sure you want to remove this room? All messages will be removed as well. This action cannot be undone.',
      action: 'Delete',
      color: 'mat-warn'
    }

    const dialogRef = this.dialog.open(ConfirmDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(data => {
      if(data != undefined &&
        data.response === true)
      {
        this.deleteRoom(room.roomId);
      }
    });
  }

}
