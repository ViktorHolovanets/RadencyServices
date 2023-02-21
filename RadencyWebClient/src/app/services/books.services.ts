import {Injectable} from "@angular/core";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {IBook} from "../models/IBook";
import {ErrorService} from "./error.service";
import {IBookAllInfo} from "../models/IBookAllInfo";

@Injectable({
  providedIn: 'root'
})
export class BooksServices {
  constructor(private http: HttpClient, private errorService: ErrorService) {
  }

  getAll(): Observable<IBook[]> {
    return this.http.get<IBook[]>('https://localhost:5000/api/books')
      .pipe(
        catchError(this.errorHandler.bind(this))
      );
  }
  getBook(id:number): Observable<IBookAllInfo[]> {
    return this.http.get<IBookAllInfo[]>('https://localhost:5000/api/books/'+id.toString())
      .pipe(
        catchError(this.errorHandler.bind(this))
      );
  }
  private errorHandler(error: HttpErrorResponse) {
    this.errorService.handle(error.message)
    return throwError(() => error.message)
  }
}
