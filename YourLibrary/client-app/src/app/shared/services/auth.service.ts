import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, of } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router'
import jwt_decode from 'jwt-decode';

//Services
import { UserService } from '../services/user-service/user.service'

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

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasToken());

  get isAuthenticated$() {
    return this.isAuthenticatedSubject.asObservable();
  }
  constructor(
    private http: HttpClient,
    private cookieService: CookieService,
    private router: Router,
    private userService: UserService
  ) {
    this.checkTokenValidityAndRefresh().subscribe();
  }

  login(loginRequest: UserLoginRequestModel): Observable<any> {
    return this.http.post<any>(this.loginUrl, loginRequest).pipe(
      map((response: any) => response.jwt),
      tap((token: string) => {
        const expirationDate = new Date();
        expirationDate.setMinutes(expirationDate.getMinutes() + 60);
        this.cookieService.set('jwtToken', token, expirationDate);
        this.isAuthenticatedSubject.next(true); // Mark the user as authenticated
        this.userService.getCurrentUser().subscribe();
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
    this.userService.setCurrentUser(null);
    this.isAuthenticatedSubject.next(false); // Add this line
    this.router.navigateByUrl('/login');
  }

  getTokenFromCookie(): string {
    return this.cookieService.get('jwtToken');
  }

  private hasToken(): boolean {
    return !!this.cookieService.get('jwtToken');
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
    return this.isAuthenticatedSubject.value;
  }

  checkTokenValidityAndRefresh(): Observable<boolean> {
    const token = this.cookieService.get('jwtToken');

    if (!token) {
      return of(false); // of() is used to create a new Observable that emits the specified value.
    }

    // Try to decode the token and check its expiry date.
    try {
      const decodedToken = jwt_decode<{ exp: number }>(token);
      const expirationDate = decodedToken.exp * 1000; // Convert to milliseconds.
      const now = new Date().getTime(); // Current time in milliseconds.

      // If current date is greater than the expiration date, the token is expired.
      if (now > expirationDate) {
        return this.refreshToken().pipe(
          map(newToken => {
            console.log('Token refreshed!');
            this.userService.getCurrentUser();
            this.isAuthenticatedSubject.next(true); // Add this line
            return true; // Token refresh successful.
          }),
          catchError(error => {
            console.error('Failed to refresh token!', error);
            this.isAuthenticatedSubject.next(false); // Add this line
            return of(false); // Token refresh failed.
          })
        );
      } else {
        this.isAuthenticatedSubject.next(true); // Add this line
        return of(true); // Token is not expired.
      }
    } catch (error) {
      this.isAuthenticatedSubject.next(false); // Add this line
      return of(false); // Token decoding failed.
    }
  }

}
