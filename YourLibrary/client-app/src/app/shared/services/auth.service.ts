import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';

//Models
import { UserLoginRequestModel } from '../models/auth/userloginrequestmodel';
import { UserRegisterRequestModel } from '../models/auth/userregisterrequestmodel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loginUrl = 'https://localhost:7007/api/account/login';
  private registerUrl = 'https://localhost:7007/api/Account/Register';

  constructor(private http: HttpClient) { }

  login(loginRequest: UserLoginRequestModel): Observable<any> {
    return this.http.post<any>(this.loginUrl, loginRequest);
  }

  register(registerRequest: UserRegisterRequestModel): Observable<any> {
    console.log("Received data: ", registerRequest);
    return this.http.post<any>(this.registerUrl, registerRequest).pipe(
      catchError((error) => {
        console.error("Registration error:", error);
        throw error; // Re-throw the error to propagate it to the caller
      })
    );
  }


}
