import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialogConfig, MatDialog } from '@angular/material';

import { SphereService } from '../sphere.service';
import { RoomService } from '../room.service';
import { MessageService } from '../message.service';

import { Sphere } from '../sphere';
import { Room } from '../room';
import { Message } from '../message';

import { MessageDialogComponent } from '../message-dialog/message-dialog.component';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

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

  private field: string = '';

  constructor(
    private dialog: MatDialog,
    private sphereService: SphereService,
    private roomService: RoomService,
    private messageService: MessageService,
    private route: ActivatedRoute) { }

  getMessages(): void {
    this.roomService.getRoomMessages(this.roomId).subscribe(_ => this.messages = _);
  }

  addMessage(id: number, content: string): void {
    const message: Message = { messageId: 0, content: content, lastModified: new Date() };
    this.roomService.postRoomMessage(id, message).subscribe(_ => this.messages.push(_));
  }

  changeMessage(id: number, content: string): void {
    const message: Message = this.messages.find(_ => _.messageId === id);
    message.content = content;
    message.lastModified = new Date();
    this.messageService.putMessage(id, message).subscribe(_ => {
      this.messages.splice(this.messages.indexOf(this.messages.find(m => m.messageId === _.messageId)), 1, _);
    });
  }

  deleteMessage(id: number): void {
    this.messageService.deleteMessage(id).subscribe(_ => {
      this.messages.splice(this.messages.indexOf(this.messages.find(m => m.messageId === id)), 1);
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
    if(this.field !== '')
    {
      this.addMessage(this.roomId, this.field);
      this.field = '';
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
      details: `Are you sure you want to remove this message:<br />${message.content}`,
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
