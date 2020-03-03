import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-sphere-dialog',
  templateUrl: './sphere-dialog.component.html'
})
export class SphereDialogComponent implements OnInit {

  name: string;
  description: string;
  dialogTitle: string;

  constructor(
    private dialogRef: MatDialogRef<SphereDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
    this.name = data.name;
    this.description = data.description;
    this.dialogTitle = data.dialogTitle;
  }

  ngOnInit() {}

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    this.dialogRef.close({ name: this.name, description: this.description });
  }

}
