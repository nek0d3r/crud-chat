import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SphereService } from '../sphere.service';
import { RoomService } from '../room.service';
import { MessageService } from '../message.service';

import { Sphere } from '../sphere';
import { Room } from '../room';
import { Message } from '../message';

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

  constructor(
    private sphereService: SphereService,
    private roomService: RoomService,
    private messageService: MessageService,
    private route: ActivatedRoute) { }

  getMessages(): void {
    this.roomService.getRoomMessages(this.roomId).subscribe(_ => this.messages = _);
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

}
