import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-sphere-dialog',
  templateUrl: './sphere-dialog.component.html'
})
export class SphereDialogComponent implements OnInit {

  name: '';
  description: '';

  constructor(
    private dialogRef: MatDialogRef<SphereDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) { }

  ngOnInit() {}

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    this.dialogRef.close({ name: this.name, description: this.description });
  }

}
