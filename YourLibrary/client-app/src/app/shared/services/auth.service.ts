import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router'
import jwt_decode from 'jwt-decode';



//Models
import { UserLoginRequestModel } from '../models/auth/userloginrequestmodel';
import { UserRegisterRequestModel } from '../models/auth/userregisterrequestmodel';
import { map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loginUrl = 'https://localhost:7007/api/account/login';
  private registerUrl = 'https://localhost:7007/api/Account/Register';
  private refreshUrl = 'https://localhost:7007/api/Account/refresh-token';
  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private router: Router,
  ) { }

  login(loginRequest: UserLoginRequestModel): Observable<any> {
    return this.http.post<any>(this.loginUrl, loginRequest).pipe(
      map((response: any) => response.jwt),
      tap((token: string) => {
        const expirationDate = new Date();
        expirationDate.setMinutes(expirationDate.getMinutes() + 60);
        this.cookieService.set('jwtToken', token, expirationDate)
      }
      )
    );
  }

  register(registerRequest: UserRegisterRequestModel): Observable<any> {
    return this.http.post<any>(this.registerUrl, registerRequest).pipe(
      catchError((error) => {
        console.error("Registration error:", error);
        throw error; // Re-throw the error to propagate it to the caller
      })
    );
  }

  logout(): void {
    this.cookieService.delete('jwtToken');
    this.router.navigateByUrl('/login');
  }

  getTokenFromCookie(): string {
    return this.cookieService.get('jwtToken');
  }

  refreshToken(): Observable<string> {
    const token = this.cookieService.get('jwtToken');

    return this.http.post<string>(this.refreshUrl, { token }, { responseType: 'text' as 'json' })
      .pipe(
        tap(newToken => {
          this.cookieService.set('jwtToken', newToken, { sameSite: 'Lax' });
        })
    )
  }

  isAuthenticated(): boolean {
    const token = this.cookieService.get('jwtToken');

    if (!token) {
      return false;
    }

    // Try to decode the token and check its expiry date.
    try {
      const decodedToken = jwt_decode<{ exp: number }>(token);
      const expirationDate = decodedToken.exp * 1000; // Convert to milliseconds.
      const now = new Date().getTime(); // Current time in milliseconds.

      // If current date is greater than the expiration date, the token is expired.
      if (now > expirationDate) {
        this.refreshToken().subscribe(
          newToken => {
            console.log('Token refreshed!');
          },
          error => {
            console.error('Failed to refresh token!', error);
          }
        );
        return false;
      }
    } catch (error) {
      return false;
    }

    // If we got this far, the token exists and hasn't expired.
    return true;
  }





}
