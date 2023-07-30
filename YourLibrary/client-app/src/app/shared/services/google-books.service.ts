import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { catchError, Observable, of, throwError } from 'rxjs';

import { BookSearchResponseModel } from '../../shared/models/booksearchresponsemodel'

@Injectable({
  providedIn: 'root'
})
export class GoogleBooksService {

  private searchUrl = 'https://localhost:7007/search';

  constructor(private http: HttpClient) { }

  getBooks(query: string, startIndex: number = 0, pageSize: number = 10): Observable<BookSearchResponseModel> {
    const params = new HttpParams()
      .set('query', query)
      .set('startIndex', startIndex.toString())
      .set('maxResults', pageSize.toString())
    return this.http.get<BookSearchResponseModel>(this.searchUrl, { params: params }).pipe(
      catchError((error) => {
        console.error("Error retrieving books: ", error);
        return throwError(error);
      })
    )
  }


}
