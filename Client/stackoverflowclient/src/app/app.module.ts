import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StackComponent } from './stack/stack.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragDropModule } from '@angular/cdk/drag-drop';

import {
  MatRadioModule,
  MatDatepickerModule,
  MatInputModule,
  MatNativeDateModule,
  MatAutocompleteModule,
  MatCheckboxModule,
  MatCardModule,
  MatSlideToggleModule
} from '@angular/material';
import { HttpErrorHandler } from './shared/services/http-error-handler.service';
import { HttpClientModule, /* other http imports */ } from "@angular/common/http";
import { MessageService } from './shared/services/message.service';


@NgModule({
  declarations: [
    AppComponent,
    StackComponent
  ],
  imports: [ 
    BrowserModule,
    MatRadioModule,
    DragDropModule,
    AppRoutingModule, 
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatInputModule,
    MatCardModule,
    MatNativeDateModule,
    BrowserAnimationsModule,
    MatAutocompleteModule,
    MatCheckboxModule,
    MatSlideToggleModule
  ],
  providers: [MatDatepickerModule, HttpErrorHandler, MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
