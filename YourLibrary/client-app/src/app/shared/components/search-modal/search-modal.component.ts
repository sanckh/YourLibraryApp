import { Component, Input, EventEmitter, Output } from '@angular/core';
import { SweetAlertService } from '../../services/sweetalert.service'

@Component({
  selector: 'app-search-modal',
  templateUrl: './search-modal.component.html',
  styleUrls: ['./search-modal.component.css']
})
export class SearchModalComponent {

  @Input() searchResults: any[];
  @Output() closeModal = new EventEmitter<void>();

  constructor(private sweetAlertService: SweetAlertService) { }

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
}
