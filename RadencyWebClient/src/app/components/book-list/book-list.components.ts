import {Component, OnInit} from "@angular/core";
import {IBookInfo} from "../../models/IBookInfo";
import {BooksServices} from "../../services/books.services";
import {ModalService} from "../../services/modal.service";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.components.html',
})
export class BookListComponent implements OnInit {
  title = 'RadencyWebClient';

  loading = false;

  constructor(public booksService: BooksServices) {
  }

  ngOnInit(): void {
    this.loading = true;
    this.booksService.getAll().subscribe(books => {
        this.loading = false;
      }
    )
  }

}
