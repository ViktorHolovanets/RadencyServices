import {Injectable} from "@angular/core";
import {HttpClient, HttpErrorResponse, HttpHeaders} from "@angular/common/http";
import {catchError, Observable, tap, throwError} from "rxjs";
import {IBookInfo} from "../models/IBookInfo";
import {ErrorService} from "./error.service";
import {IBookAllInfo} from "../models/IBookAllInfo";
import {FormGroup} from "@angular/forms";
import {IBook} from "../models/IBook";

@Injectable({
  providedIn: 'root'
})
export class BooksServices {
  constructor(private http: HttpClient, private errorService: ErrorService) {
  }
  books: IBookInfo[] = [];
  getAll(): Observable<IBookInfo[]> {
    return this.http.get<IBookInfo[]>('https://localhost:5000/api/books')
      .pipe(
        tap(books=>this.books=books),
        catchError(this.errorHandler.bind(this))
      );
  }
  getBook(id:number): Observable<IBookAllInfo[]> {
    return this.http.get<IBookAllInfo[]>('https://localhost:5000/api/books/'+id.toString())
      .pipe(
        catchError(this.errorHandler.bind(this))
      );
  }
  saveBook(form:any): Observable<IBook> {
    const headers = new HttpHeaders()
      .set('content-type', 'application/json')
      .set('Access-Control-Allow-Origin', '*');
    console.log(form);
    return this.http.post<IBook>('https://localhost:5000/api/books/save',form,{headers:headers} )
      .pipe(
        tap(book=>{
         let newBook= this.books.find(b=>b.id==book.id);
          let n=new class implements IBookInfo {
            author: string;
            cover: string;
            id: number;
            rating: number;
            reviewsNumber: number;
            title: string;
          }
          n.id=book.id
          n.author=book.author
          n.title=book.title
          n.cover=book.cover
          n.rating=0
          n.reviewsNumber=0
         if(newBook===undefined){
           this.books.push(n)
         }
         else{
           newBook.id=book.id
           newBook.author=book.author
           newBook.title=book.title
           newBook.cover=book.cover
         }
        }),
        catchError(this.errorHandler.bind(this))
      );
  }
  private errorHandler(error: HttpErrorResponse) {
    this.errorService.handle(error.message)
    return throwError(() => error.message)
  }
}
