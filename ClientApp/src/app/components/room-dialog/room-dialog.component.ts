import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';

import { RoomForm } from '@app/models/room/room-form';

@Component({
  selector: 'app-room-dialog',
  templateUrl: './room-dialog.component.html'
})
export class RoomDialogComponent implements OnInit {

  dialogTitle: string;

  roomForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
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
    this.roomForm = this.formBuilder.group(roomForm);
  }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    const roomForm: RoomForm = Object.assign({}, this.roomForm.value);
    this.dialogRef.close({ title: roomForm.title });
  }

}
