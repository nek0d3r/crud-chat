import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { FormGroup, Validators, FormControl } from '@angular/forms';

import { MessageForm } from '@app/models/message/message-form';

@Component({
  selector: 'app-message-dialog',
  templateUrl: './message-dialog.component.html'
})
export class MessageDialogComponent implements OnInit {

  dialogTitle: string;

  messageForm: FormGroup;

  constructor(
    private dialogRef: MatDialogRef<MessageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) data) {
    const messageForm: MessageForm = {
      content: data.content
    };
    this.createForm(messageForm);
    this.dialogTitle = data.dialogTitle;
  }

  ngOnInit() { }

  createForm(messageForm: MessageForm): void {
    this.messageForm = new FormGroup({
      content: new FormControl(messageForm.content, [Validators.required])
    });
  }

  close(): void {
    this.dialogRef.close();
  }

  save(): void {
    if(this.messageForm.valid)
    {
      const messageForm: MessageForm = Object.assign({}, this.messageForm.value);
      this.dialogRef.close({ content: messageForm.content });
    }
  }

}
