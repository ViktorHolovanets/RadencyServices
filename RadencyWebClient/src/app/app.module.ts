import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {BookListComponent} from "./componets/book-list/book-list.components";
import {BookItemComponent} from "./componets/book-list-item/book-list-item.components";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {EditBookComponents} from "./componets/edit-book/edit-book.components";
import {ViewBookComponents} from "./componets/view-book/view-book.components";
import {HttpClientModule} from "@angular/common/http";

@NgModule({
  declarations: [
    AppComponent,
    BookListComponent,
    BookItemComponent,
    EditBookComponents,
    ViewBookComponents
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
