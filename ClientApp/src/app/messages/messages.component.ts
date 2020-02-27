import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { MessageService } from '../message.service';

import { Message } from '../message';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  private sphereId: number;
  private roomId: number;

  private messages: Message[];

  constructor(private messageService: MessageService, private route: ActivatedRoute) { }

  getMessages(): void {
    this.messageService.getMessages().subscribe(_ => this.messages = _);
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(_ => {
      this.sphereId = parseInt(_.get('id1'));
      this.roomId = parseInt(_.get('id2'));
    });
    this.getMessages();
  }

}
