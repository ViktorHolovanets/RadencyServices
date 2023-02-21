import {Component, DoCheck} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {EditBookService} from "../../services/edit-book.service";
import {BooksServices} from "../../services/books.services";

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.components.html',
})
export class EditBookComponents implements DoCheck {
  constructor(public editService: EditBookService, private bookService: BooksServices) {
  }

  ngDoCheck(): void {
    if (this.editService.isEdit$.value && this.editService.isLoadedBook$.value) {
      this.bookService.getBook(this.editService.id$.value).subscribe(book => {
        if (book.length > 0) {
          this.form = new FormGroup({
            id: new FormControl<number>(book[0].id),
            title: new FormControl<string>(book[0].title, [Validators.required]),
            author: new FormControl<string>(book[0].author, [Validators.required]),
            cover: new FormControl<string>(book[0].cover, [Validators.required]),
            genre: new FormControl<string>(book[0].genre, [Validators.required]),
            content: new FormControl<string>(book[0].content, [Validators.required])
          })
        }
        this.editService.isLoadedBook$.next(false);
      })

    }
  }


  form: FormGroup = new FormGroup({
    id: new FormControl<number>(0),
    title: new FormControl<string>('', [Validators.required]),
    author: new FormControl<string>('', [Validators.required]),
    cover: new FormControl<string>('', [Validators.required]),
    genre: new FormControl<string>('', [Validators.required]),
    content: new FormControl<string>('', [Validators.required])
  })

  submit() {
    let obj = this.form.value;
    console.log(obj)
    if(!this.editService.isEdit$.value){
      delete obj.id
    }
    console.log(obj)
    this.bookService.saveBook(obj).subscribe(book=>
      console.log(book));
  }

  clear() {
    this.form = new FormGroup({
      id: new FormControl<number>(0),
      title: new FormControl<string>('', [Validators.required]),
      author: new FormControl<string>('', [Validators.required]),
      cover: new FormControl<string>('', [Validators.required]),
      genre: new FormControl<string>('', [Validators.required]),
      content: new FormControl<string>('', [Validators.required])
    })
    this.editService.clear();
  }


}
