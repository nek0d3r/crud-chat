import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {

  private sphereId: number;
  
  private rooms = [
    { id: 1, title: "Room1" },
    { id: 2, title: "Room2" },
    { id: 3, title: "Room3" }
  ]

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.paramMap.subscribe(_ => {
      this.sphereId = parseInt(_.get('id'));
    })
  }

}
