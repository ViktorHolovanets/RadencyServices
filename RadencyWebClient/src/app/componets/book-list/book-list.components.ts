import {Component} from "@angular/core";
import {IBook} from "../../models/IBook";
import {books} from "../../data/testBook";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.components.html',
})
export class BookListComponent {
  title = 'RadencyWebClient';
  books: IBook[]=books;
  func(): void {
    this.title = "Radencyy"
  }
}
