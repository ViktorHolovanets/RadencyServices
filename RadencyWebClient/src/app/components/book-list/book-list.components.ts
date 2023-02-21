import {Component, OnInit} from "@angular/core";
import {BooksServices} from "../../services/books.services";

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
    this.all()
  }

  all() {
    this.loading = true;
    this.booksService.getAll().subscribe(books => {
        this.loading = false;
      }
    )
  }
  recommended(){
    this.loading = true;
    this.booksService.getRecommended().subscribe(books => {
        this.loading = false;
      }
    )
  }
}
