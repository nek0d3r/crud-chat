import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-room-dialog',
  templateUrl: './room-dialog.component.html'
})
export class RoomDialogComponent implements OnInit {

  title: string;
  dialogTitle: string;

  constructor(
    private dialogRef: MatDialogRef<RoomDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
    this.title = data.title;
    this.dialogTitle = data.dialogTitle;
  }

  ngOnInit() { }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    this.dialogRef.close({ title: this.title });
  }

}
