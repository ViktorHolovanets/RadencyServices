import {Injectable} from '@angular/core';
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class ModalService {
  isVisible$ = new BehaviorSubject<boolean>(false)
  id$ = new BehaviorSubject<number>(-1)

  open(id: number) {
    this.id$.next(id)
    this.isVisible$.next(true)
  }

  close() {
    this.isVisible$.next(false)
  }
}
