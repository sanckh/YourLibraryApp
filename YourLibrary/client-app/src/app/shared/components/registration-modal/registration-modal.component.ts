import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import {SweetAlertService} from '../../services/sweetalert.service'; 
import { UserRegisterRequestModel } from '../../models/auth/userregisterrequestmodel';

@Component({
  selector: 'app-registration-modal',
  templateUrl: './registration-modal.component.html',
  styleUrls: ['./registration-modal.component.css']
})
export class RegistrationModalComponent implements OnInit {

  @Output() closeModal = new EventEmitter<void>();
  @Output() register = new EventEmitter<UserRegisterRequestModel>();

  public formGroup: FormGroup;

  constructor(
    private sweetAlertService: SweetAlertService,
    private formBuilder: FormBuilder
    ) {
  }

  private initializeForm(): void {
    this.formGroup = this.formBuilder.group({
      email: ['', [Validators.email, Validators.required]],
      password: ['', [Validators.minLength(5), Validators.required]],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      dateOfBirth: [''] // Optional field, no validators added
    });
  }



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
      const registerRequest: UserRegisterRequestModel = {
        email: this.formGroup.value.email,
        password: this.formGroup.value.password,
        firstName: this.formGroup.value.firstName,
        lastName: this.formGroup.value.lastName,
        dateOfBirth: this.formGroup.value.dateOfBirth
      };
      this.register.emit(registerRequest);
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

  ngOnInit(): void {
    this.initializeForm();
  }

}
