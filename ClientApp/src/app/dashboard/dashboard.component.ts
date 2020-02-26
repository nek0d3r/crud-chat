import { Component, OnInit } from '@angular/core';
import { MatSliderModule } from '@angular/material';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  private spheres = [
    { title: "Test1", rooms: 3 },
    { title: "Test2", rooms: 1 },
    { title: "Test3", rooms: 5 }
  ]

  constructor() { }

  ngOnInit() {
  }

}
