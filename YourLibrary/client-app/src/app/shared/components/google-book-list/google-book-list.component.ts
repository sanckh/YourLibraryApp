import { Component, Input, OnInit } from '@angular/core';
import { GoogleBooksService } from '../../services/google-books.service'
import { BookSearchResponseModel, Item } from '../../models/booksearchresponsemodel'
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-google-book-list',
  templateUrl: './google-book-list.component.html',
  styleUrls: ['./google-book-list.component.css']
})
export class GoogleBookListComponent implements OnInit {

  query: string;
  books: Item[];

  constructor(
    private googleBooksService: GoogleBooksService,
    private route: ActivatedRoute,
  ) { }

  searchBooks() {
    if (this.query && this.query.trim() !== '') {
      this.googleBooksService.getBooks(this.query).subscribe((data: BookSearchResponseModel) => {
      if (data && data.items) {
        this.books = data.items;
      } else {
        console.log('No items found in the response data');
      }
    }, error => {
      console.error('Error: ', error);
    });
    }
  }



  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.query = params['query'];
      this.searchBooks();
    })
  }

}
