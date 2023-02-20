import {Component, OnInit} from "@angular/core";
import {IBook} from "../../models/IBook";
import {books} from "../../data/testBook";
import {BooksServices} from "../../services/books.services";

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.components.html',
})
export class BookListComponent implements OnInit{
  title = 'RadencyWebClient';
  books: IBook[]=[];

  constructor(private  booksService:BooksServices) {
  }

  ngOnInit(): void {
    this.booksService.getAll().subscribe(books=>
      this.books=books
    )
  }

}
