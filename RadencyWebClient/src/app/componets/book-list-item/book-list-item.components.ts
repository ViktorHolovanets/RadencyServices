import {Component, Input} from "@angular/core";
import {IBook} from "../../models/IBook";

@Component({
  selector: 'app-book-list-item',
  templateUrl: './book-list-item.components.html',
})
export class BookItemComponent {
  @Input() book:IBook
}
