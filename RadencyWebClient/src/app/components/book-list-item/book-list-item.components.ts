import {Component, Input} from "@angular/core";
import {IBookInfo} from "../../models/IBookInfo";
import {ModalService} from "../../services/modal.service";
import {EditBookService} from "../../services/edit-book.service";

@Component({
  selector: 'app-book-list-item',
  templateUrl: './book-list-item.components.html',
})
export class BookItemComponent {
  @Input() book: IBookInfo

  constructor(private modalService: ModalService, private  editService:EditBookService) {
  }
  viewInfo(){
    this.modalService.open(this.book.id)
  }
  edit(){
    this.editService.edit(this.book.id)
  }
}
