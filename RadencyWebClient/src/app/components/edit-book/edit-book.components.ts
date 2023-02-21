import {Component, DoCheck, OnChanges, SimpleChanges} from "@angular/core";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {EditBookService} from "../../services/edit-book.service";

@Component({
  selector: 'app-edit-book',
  templateUrl: './edit-book.components.html',
})
export class EditBookComponents implements DoCheck {
  constructor(public editService: EditBookService) {
  }

  ngDoCheck(): void {
       console.log(this.editService.isEdit$.value);
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
    console.log(this.form.value)
    let obj = this.form.value
    delete obj.id
    console.log(obj)
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
  }


}
