import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  private spheres = [
    { id: 1, title: "Test1", rooms: 3 },
    { id: 2, title: "Test2", rooms: 1 },
    { id: 3, title: "Test3", rooms: 5 }
  ]

  constructor() { }

  ngOnInit() {
  }

}
