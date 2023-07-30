import { Component, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-search-modal',
  templateUrl: './search-modal.component.html',
  styleUrls: ['./search-modal.component.css']
})
export class SearchModalComponent {

  @Input() searchResults: any[];
  @Output() closeModal = new EventEmitter<void>();

  onCloseClicked = () => {
    this.closeModal.emit();
  }
}
