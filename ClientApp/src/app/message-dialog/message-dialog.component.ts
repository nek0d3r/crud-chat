import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

@Component({
  selector: 'app-message-dialog',
  templateUrl: './message-dialog.component.html'
})
export class MessageDialogComponent implements OnInit {

  content: string;
  dialogTitle: string;

  constructor(
    private dialogRef: MatDialogRef<MessageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
    this.content = data.content;
    this.dialogTitle = data.dialogTitle;
  }

  ngOnInit() { }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    this.dialogRef.close({ content: this.content });
  }

}
