import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, Observable } from 'rxjs';
import { CurrentUserModel } from '../../models/currentuser-model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private currentUserUrl = 'https://localhost:7007/api/user/current'; // Replace with your API URL

  constructor(
    private http: HttpClient,
    private cookieService: CookieService
    ) { }

  getCurrentUser(): Observable<CurrentUserModel> {
    const token = this.cookieService.get('jwtToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<CurrentUserModel>(this.currentUserUrl, { headers }).pipe(
      catchError((error) => {
        console.error("Error getting current user:", error);
        throw error;
      })
    );
  }
}
