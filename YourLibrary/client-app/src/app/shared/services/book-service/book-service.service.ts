import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BookModel } from '../../models/book-model';

@Injectable({
  providedIn: 'root'
})
export class BookService {

  private bookUrl = 'https://localhost:7007/api/book'; // Replace with your API URL


  constructor(private http: HttpClient) { }

  getRecentlyAddedBooks(): Observable<BookModel[]> {
    return this.http.get<BookModel[]>(`${this.bookUrl}/recentlyadded`);
  }
}
