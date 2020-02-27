import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { RoomService } from '../room.service';

import { Room } from '../room';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {

  private sphereId: number;
  
  private rooms: Room[];

  constructor(private roomService: RoomService, private route: ActivatedRoute) {}

  getRooms(): void {
    this.roomService.getRooms().subscribe(_ => this.rooms = _)
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(_ => {
      this.sphereId = parseInt(_.get('id'));
    })
    this.getRooms();
  }

}
