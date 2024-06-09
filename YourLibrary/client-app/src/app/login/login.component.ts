import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {SweetAlertService} from '../shared/services/sweetalert.service'; 
import {Router} from '@angular/router'
// Services
import { AuthService } from '../shared/services/auth.service';
import { UserService } from '../shared/services/user-service/user.service'

// Models
import { UserLoginRequestModel } from '../shared/models/auth/userloginrequestmodel';
import { UserRegisterRequestModel } from '../shared/models/auth/userregisterrequestmodel';

//Components
import {RegistrationModalComponent} from '../shared/components/registration-modal/registration-modal.component'; 


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginRequest: UserLoginRequestModel = {
    email: '',
    password: ''
  };

  errorMessage: string = '';
  isRegisterModalOpen: boolean = false;
  registerRequest: UserRegisterRequestModel = {
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    dateOfBirth: ''
  };

  registerErrorMessage: string = '';


  constructor(
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private sweetAlertService: SweetAlertService,
    private router: Router,
    private userService: UserService,
  ) { }

  ngOnInit(): void {
  }

  login(): void {
    this.authService.login(this.loginRequest).subscribe(
      response => {
        // Handle successful login here
        // Redirect the user to the desired page
        this.router.navigate(['/home']);
      },
      error => {
        // Handle login error
        this.errorMessage = error.message;
      }
    );
  }


  openRegisterModal(): void {
    this.isRegisterModalOpen = true;
  }

  closeRegisterModal(): void {
    this.isRegisterModalOpen = false;
    // Reset the register form here if needed
    this.registerRequest = {
      email: '',
      password: '',
      firstName: '',
      lastName: '',
      dateOfBirth: ''
    };
    this.registerErrorMessage = '';
  }

  register(registerRequest: UserRegisterRequestModel): void {
    registerRequest.dateOfBirth = new Date(registerRequest.dateOfBirth).toISOString();

    this.authService.register(registerRequest).subscribe(
      (response) => {
        this.sweetAlertService.showSuccessAlert('Success', 'Registration completed successfully')
        .then((result) => {
          // Handle the user's action after the alert is closed
          if (result.isConfirmed) {
            // Perform any necessary actions after successful registration
            this.closeRegisterModal();
          }
        });
      },
      error => {
        // Handle registration error
        console.error("Registration error: ", error)
        if (error.status === 400 && error.error.message === "This email exists!") {
          this.registerErrorMessage = "Email already exists! Please try again with a different email address."
          console.log(error.message)
        }
        else {
          this.registerErrorMessage = "An error occurred while registering. Please try again later."
        }
      }
    );
  }
}
