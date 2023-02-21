import {Component, OnInit} from "@angular/core";
import {IBook} from "../../models/IBook";
import {BooksServices} from "../../services/books.services";
import {ModalService} from "../../services/modal.service";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.components.html',
})
export class BookListComponent implements OnInit {
  title = 'RadencyWebClient';
  books: IBook[] = [];
  loading = false;

  constructor(private booksService: BooksServices) {
  }

  ngOnInit(): void {
    this.loading = true;
    this.booksService.getAll().subscribe(books => {
        this.books = books;
        this.loading = false;
      }
    )
  }

}
