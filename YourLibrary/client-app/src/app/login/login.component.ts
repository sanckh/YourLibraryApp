import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

// Services
import { AuthService } from '../shared/services/auth.service';

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
  ) { }

  ngOnInit(): void {
  }

  login(): void {
    this.authService.login(this.loginRequest).subscribe(
      response => {
        // Handle successful login here
        // Access the JWT token from the response
        const token = response.jwt;
        console.log("Success!")
        // You can store the token in localStorage or a cookie for auth purposes
        // Redirect the user to the desired page
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

  register(): void {
    this.authService.register(this.registerRequest).subscribe(
      
      response => {
        console.log("Registration Successful!")
        // Handle successful registration here
        // Display a success message or redirect the user
        this.closeRegisterModal();
      },
      error => {
        // Handle registration error
        console.error("Registration error: ", error)
        if (error.status === 400 && error.error.message === "This email exists!") {
          this.registerErrorMessage = "Email already exists! Please try again with a different email address."
        }
        else {
          this.registerErrorMessage = "An error occurred while registering. Please try again later."
        }
      }
    );
  }
}
