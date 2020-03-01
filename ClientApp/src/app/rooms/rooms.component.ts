import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SphereService } from '../sphere.service';
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

  constructor(
    private sphereService: SphereService,
    private roomService: RoomService,
    private route: ActivatedRoute) {}

  getRooms(): void {
    this.sphereService.getSphereRooms(this.sphereId).subscribe(_ => this.rooms = _)
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(_ => {
      this.sphereId = parseInt(_.get('id'));
    })
    this.getRooms();
  }

}
