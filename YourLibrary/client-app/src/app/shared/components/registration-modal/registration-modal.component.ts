import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import {SweetAlertService} from '../../services/sweetalert.service'; 

@Component({
  selector: 'app-registration-modal',
  templateUrl: './registration-modal.component.html',
  styleUrls: ['./registration-modal.component.css']
})
export class RegistrationModalComponent implements OnInit {

  @Output() closeModal = new EventEmitter<void>();
  @Output() register = new EventEmitter<void>();

  private controls = {
    email: new UntypedFormControl(),
    password: new UntypedFormControl(),
    firstName: new UntypedFormControl(),
    lastName: new UntypedFormControl(),
    dateOfBirth: new UntypedFormControl()
  }

  public formGroup = new UntypedFormGroup(this.controls);

  constructor(private sweetAlertService: SweetAlertService) {

    this.setValidators();
  }

  ngOnInit(): void {
  }

  //Public Properties
  get email() { return this.controls.email; }
  get password() { return this.controls.password; }
  get firstName() { return this.controls.firstName; }
  get lastName() { return this.controls.lastName; }
  get dateOfBirth() { return this.controls.dateOfBirth; }

  get canSave() { return this.formGroup.valid; }

  onCloseClicked = () => {
    if (this.formGroup.dirty) {
      this.sweetAlertService.showConfirmationAlert('Are you sure?', 'Clicking yes will clear this form')
        .then((result) => {
          if (result.isConfirmed) {
            //Close the modal
            this.closeModal.emit();
          }
        });
    } else {
      //close the modal directly if no changes were made
      this.closeModal.emit();
    }
  }
 

  onRegisterClicked = () => {
    if (this.canSave) {
      this.sweetAlertService.showSuccessAlert('Success', 'Registration completed successfully')
        .then((result) => {
          // Handle the user's action after the alert is closed
          if (result.isConfirmed) {
            // Perform any necessary actions after successful registration
            this.register.emit();
          }
        });
    } else {
      this.sweetAlertService.showErrorAlert('Error', 'Please fill in all required fields');
    }
  }

  getErrorMessage(controlName: string): string {
    const control = this.formGroup.get(controlName);

    if (control && control.errors) {
      if (control.errors['required']) {
        return 'This field is required.';
      }

      if (control.errors['email']) {
        return 'Please enter a valid email address.';
      }

      // Add more error messages for other validation rules if needed

      // Return an empty string if no specific error message is defined
      return '';
    }

    return '';
  }



  private setValidators() {
    this.controls.email.setValidators([Validators.email, Validators.required]);
    this.controls.password.setValidators([Validators.minLength(5), Validators.required]);
    this.controls.firstName.setValidators([Validators.required]);
    this.controls.lastName.setValidators([Validators.required]);
    this.controls.dateOfBirth.setValidators([Validators.required]);

  }


}
