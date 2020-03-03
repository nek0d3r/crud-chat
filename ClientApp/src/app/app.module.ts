import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FlexLayoutModule } from '@angular/flex-layout';
import {
  MatButtonModule,
  MatCardModule,
  MatToolbarModule,
  MatIconModule,
  MatInputModule,
  MatFormFieldModule,
  MatListModule,
  MatDialogModule,
  MatMenuModule
} from '@angular/material';

import { AppComponent } from '@app/app.component';
import { AppRoutingModule } from '@app/app-routing.module';
import { DashboardComponent } from '@app/components/dashboard/dashboard.component';
import { RoomsComponent } from '@app/components/rooms/rooms.component';
import { MessagesComponent } from '@app/components/messages/messages.component';
import { SphereDialogComponent } from '@app/components/sphere-dialog/sphere-dialog.component';
import { ConfirmDialogComponent } from '@app/components/confirm-dialog/confirm-dialog.component';
import { RoomDialogComponent } from '@app/components/room-dialog/room-dialog.component';
import { MessageDialogComponent } from '@app/components/message-dialog/message-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    RoomsComponent,
    MessagesComponent,
    SphereDialogComponent,
    ConfirmDialogComponent,
    RoomDialogComponent,
    MessageDialogComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FlexLayoutModule,
    MatButtonModule,
    MatCardModule,
    MatToolbarModule,
    MatIconModule,
    MatInputModule,
    MatFormFieldModule,
    MatListModule,
    MatDialogModule,
    MatMenuModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [SphereDialogComponent, RoomDialogComponent, MessageDialogComponent, ConfirmDialogComponent]
})
export class AppModule { }
