import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {IBook} from "../models/IBook";

@Injectable({
  providedIn:'root'
})
export class  BooksServices{
  constructor(private http:HttpClient) {
  }
  getAll():Observable<IBook[]>{
    return this.http.get<IBook[]>('https://localhost:5000/api/books');
  }
}
