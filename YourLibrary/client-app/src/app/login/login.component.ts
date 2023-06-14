import { Component, OnInit } from '@angular/core';

//Services
import { AuthService } from '../shared/services/auth.service'

//Models
import { UserLoginRequestModel } from '../shared/models/auth/userloginrequestmodel'

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

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }

  login(): void {
    this.authService.login(this.loginRequest).subscribe(
      response => {
        //Handle successful login
        // Access the JWT token from the response
        const token = response.jwt;
        //You can store the token in localStorage or a cookie for auth purposes
        //Redirect the user to the desired page
      },
      error => {
        //Handle login error
        this.errorMessage = error.message;
      }
    )
  }

}
