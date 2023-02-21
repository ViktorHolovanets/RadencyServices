import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {BookListComponent} from "./components/book-list/book-list.components";
import {BookItemComponent} from "./components/book-list-item/book-list-item.components";
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import {EditBookComponents} from "./components/edit-book/edit-book.components";
import {ViewBookComponents} from "./components/view-book/view-book.components";
import {HttpClientModule} from "@angular/common/http";
import {LoadingComponents} from "./components/loading/loading.componets";
import { ViewErrorComponent } from './components/view-error/view-error.component';
import {ReactiveFormsModule} from "@angular/forms";

@NgModule({
  declarations: [
    AppComponent,
    BookListComponent,
    BookItemComponent,
    EditBookComponents,
    ViewBookComponents,
    LoadingComponents,
    ViewErrorComponent
  ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        HttpClientModule,
        ReactiveFormsModule
    ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
