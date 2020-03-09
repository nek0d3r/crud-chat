import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialogConfig, MatDialog } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';

import { SphereService } from '@app/services/sphere/sphere.service';
import { RoomService } from '@app/services/room/room.service';
import { MessageService } from '@app/services/message/message.service';

import { Sphere } from '@app/models/sphere/sphere';
import { Room } from '@app/models/room/room';
import { Message } from '@app/models/message/message';

import { MessageDialogComponent } from '@app/components/message-dialog/message-dialog.component';
import { ConfirmDialogComponent } from '@app/components/confirm-dialog/confirm-dialog.component';

import { MessageForm } from '@app/models/message/message-form';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  private sphereId: number;
  private sphere: Sphere;

  private roomId: number;
  private room: Room;

  private messages: Message[];

  private messageForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private dialog: MatDialog,
    private sphereService: SphereService,
    private roomService: RoomService,
    private messageService: MessageService,
    private route: ActivatedRoute) {
      const messageForm: MessageForm = {
        content: ''
      };
      this.createForm(messageForm);
    }
  
  createForm(messageForm: MessageForm): void {
    this.messageForm = this.formBuilder.group(messageForm);
  }

  getMessages(): void {
    this.roomService.getRoomMessages(this.roomId).subscribe(_ => { if(_ !== null) { this.messages = _ } });
  }

  addMessage(id: number, content: string): void {
    const message: Message = { messageId: 0, content: content, lastModified: new Date() };
    this.roomService.postRoomMessage(id, message).subscribe(_ => { if(_ !== null) { this.messages.push(_) } });
  }

  changeMessage(id: number, content: string): void {
    const message: Message = this.messages.find(_ => _.messageId === id);
    message.content = content;
    message.lastModified = new Date();
    this.messageService.putMessage(id, message).subscribe(_ => {
      if(_ !== null) {
        this.messages.splice(this.messages.indexOf(this.messages.find(m => m.messageId === _.messageId)), 1, _);
      }
    });
  }

  deleteMessage(id: number): void {
    this.messageService.deleteMessage(id).subscribe(_ => {
      if(_ !== null) {
        this.messages.splice(this.messages.indexOf(this.messages.find(m => m.messageId === id)), 1);
      }
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(_ => {
      this.sphereId = parseInt(_.get('id1'));
      this.roomId = parseInt(_.get('id2'));
    });
    this.sphereService.getSphere(this.sphereId).subscribe(_ => this.sphere = _);
    this.roomService.getRoom(this.roomId).subscribe(_ => this.room = _);
    this.getMessages();
  }

  addMessageEvent(): void {
    const messageForm: MessageForm = Object.assign({}, this.messageForm.value);

    if(messageForm.content !== '')
    {
      this.addMessage(this.roomId, messageForm.content);
      const newMessageForm: MessageForm = {
        content: ''
      };
      this.messageForm = this.formBuilder.group(newMessageForm);
    }
  }

  editMessageModal(message: Message): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.autoFocus = true;
    dialogConfig.width = '500px';
    dialogConfig.data = { dialogTitle: "Edit Message", content: message.content };

    const dialogRef = this.dialog.open(MessageDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(data => {
      if(data != undefined &&
        data.content !== message.content)
      {
        this.changeMessage(message.messageId, data.content);
      }
    });
  }

  deleteMessageModal(message: Message): void {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.width = '500px';
    dialogConfig.data = {
      title: 'Remove Message',
      details: `Are you sure you want to remove this message:<br/>${message.content}`,
      action: 'Delete',
      color: 'mat-accent'
    }

    const dialogRef = this.dialog.open(ConfirmDialogComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(data => {
      if(data != undefined &&
        data.response === true)
      {
        this.deleteMessage(message.messageId);
      }
    });
  }

}
