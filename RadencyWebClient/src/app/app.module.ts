import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {BookListComponent} from "./componets/book-list/book-list.components";
import {BookItemComponent} from "./componets/book-list-item/book-list-item.components";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {EditBookComponents} from "./componets/edit-book/edit-book.components";

@NgModule({
  declarations: [
    AppComponent,
    BookListComponent,
    BookItemComponent,
    EditBookComponents
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
