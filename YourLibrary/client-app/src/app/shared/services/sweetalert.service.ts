import { Injectable } from '@angular/core';
import Swal, { SweetAlertOptions } from 'sweetalert2';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SweetAlertService {

  constructor() { }

  // Add your alert methods here

  showSuccessAlert(title: string, message: string): Promise<any> {
    const options: SweetAlertOptions = {
      title,
      text: message,
      icon: 'success',
      confirmButtonText: 'OK'
    };

    return Swal.fire(options);
  }

  showErrorAlert(title: string, message: string): Promise<any> {
    const options: SweetAlertOptions = {
      title,
      text: message,
      icon: 'error',
      confirmButtonText: 'OK'
    };

    return Swal.fire(options);
  }

  showConfirmationAlert(title: string, message: string): Promise<any> {
    const options: SweetAlertOptions = {
      title,
      text: message,
      icon: 'question',
      showCancelButton: true,
      confirmButtonText: 'Yes',
      cancelButtonText: 'No'
    };

    return Swal.fire(options);
  }


}
