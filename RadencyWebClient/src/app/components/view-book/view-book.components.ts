import {Component, OnChanges, OnInit} from "@angular/core";
import {ModalService} from "../../services/modal.service";
import {IBookAllInfo} from "../../models/IBookAllInfo";
import {BooksServices} from "../../services/books.services";
import {delay} from "rxjs";

@Component({
  selector: 'app-view-book',
  templateUrl: './view-book.components.html',
})

export class ViewBookComponents implements OnInit, OnChanges {
  constructor(public modalService: ModalService, private bookService: BooksServices) {
  }

  book: IBookAllInfo
  loading:boolean=true

  ngOnInit(): void {
    this.bookService.getBook(this.modalService.id$.value).subscribe(book => {
      if(book.length>0)
        this.book = book[0];
      this.loading=false
    })


  }

  ngOnChanges(): void {
    console.log("Change")
  }


}
