import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { RoomForm } from '@app/models/room/room-form';

@Component({
  selector: 'app-room-dialog',
  templateUrl: './room-dialog.component.html'
})
export class RoomDialogComponent implements OnInit {

  dialogTitle: string;

  roomForm: FormGroup;

  constructor(
    private dialogRef: MatDialogRef<RoomDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
    const roomForm: RoomForm = {
      title: data.title
    };
    this.createForm(roomForm);
    this.dialogTitle = data.dialogTitle;
  }

  ngOnInit() { }

  createForm(roomForm: RoomForm): void {
    this.roomForm = new FormGroup({
      title: new FormControl(roomForm.title, [Validators.required])
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    if(this.roomForm.valid)
    {
      const roomForm: RoomForm = Object.assign({}, this.roomForm.value);
      this.dialogRef.close({ title: roomForm.title });
    }
  }

}
