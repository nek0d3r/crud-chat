import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent } from './dashboard/dashboard.component';
import { RoomsComponent } from './rooms/rooms.component';
import { MessagesComponent } from './messages/messages.component';

const routes: Routes = [
  { path: 'spheres', component: DashboardComponent },
  { path: 'spheres/:id', component: RoomsComponent },
  { path: 'spheres/:id1/:id2', component: MessagesComponent },
  { path: '', redirectTo: '/spheres', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
