import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class EditBookService {
  constructor() {
  }

  isLoadedBook$ = new BehaviorSubject<boolean>(false)
  isEdit$ = new BehaviorSubject<boolean>(false)
  id$ = new BehaviorSubject<number>(0)

  edit(id: number) {
    this.isEdit$.next(true);
    this.isLoadedBook$.next(true);
    this.id$.next(id)
  }
}
