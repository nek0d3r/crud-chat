import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html'
})
export class ConfirmDialogComponent implements OnInit {

  promptTitle: string;
  promptDetails: string;
  promptAction: string;
  promptActionColor: string;

  constructor(
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
    this.promptTitle = data.title
    this.promptDetails = data.details;
    this.promptAction = data.action;
    this.promptActionColor = data.color;
  }

  ngOnInit() {}

  close(): void {
    this.dialogRef.close({ response: false });
  }

  accept(): void {
    this.dialogRef.close({ response: true });
  }

}
