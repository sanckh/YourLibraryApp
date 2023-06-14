import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

//Models
import { UserLoginRequestModel } from '../models/auth/userloginrequestmodel';
import { UserRegisterRequestModel } from '../models/auth/userregisterrequestmodel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loginUrl = '/api/account/login';
  private registerUrl = '/api/account/register';

  constructor(private http: HttpClient) { }

  login(loginRequest: UserLoginRequestModel): Observable<any> {
    return this.http.post<any>(this.loginUrl, loginRequest);
  }

  register(registerRequest: UserRegisterRequestModel): Observable<any> {
    return this.http.post<any>(this.registerUrl, registerRequest);
  }
}
