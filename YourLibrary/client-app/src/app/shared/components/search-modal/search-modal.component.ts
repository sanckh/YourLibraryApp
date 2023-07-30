import { Component, Input, EventEmitter, Output, OnInit } from '@angular/core';
import { SweetAlertService } from '../../services/sweetalert.service';
import { GoogleBooksService } from '../../services/google-books.service';
import { BookSearchResponseModel, Item } from '../../models/booksearchresponsemodel';

@Component({
  selector: 'app-search-modal',
  templateUrl: './search-modal.component.html',
  styleUrls: ['./search-modal.component.css']
})
export class SearchModalComponent implements OnInit {

  @Input() searchResults: Item[];
  @Input() query: string;
  @Output() closeModal = new EventEmitter<void>();

  pageIndex = 0;
  pageSize = 10;
  totalPages: number;


  constructor(private sweetAlertService: SweetAlertService,
              private googleBooksService: GoogleBooksService) { }


  updatePage() {
    this.googleBooksService.getBooks(this.query, this.pageIndex * 10, this.pageSize)
      .subscribe((data: BookSearchResponseModel) => {
        if (data && data.items) {
          this.searchResults = data.items;
          console.log('Total items from API', data.totalItems)
          this.totalPages = Math.ceil(data.totalItems / this.pageSize);
        }
      })
  }

  prevPage() {
    if (this.pageIndex > 0) {
      this.pageIndex--;
      this.updatePage();
    }
  }

  nextPage() {
    this.pageIndex++;
    this.updatePage();
  }


  onCloseClicked = () => {
    this.sweetAlertService.showConfirmationAlert(
      'Are you sure?',
      'You will lose any unsaved changes'
    ).then((result) => {
      if (result.isConfirmed) {
        this.closeModal.emit();

      }
    })
  }

  ngOnInit() {
    this.updatePage();
  }
}
