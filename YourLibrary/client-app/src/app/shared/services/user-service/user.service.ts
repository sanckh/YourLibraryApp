import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, catchError, Observable, tap } from 'rxjs';
import { CurrentUserModel } from '../../models/currentuser-model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private currentUserUrl = 'https://localhost:7007/api/user/current'; // Replace with your API URL

  currentUserSubject = new BehaviorSubject<CurrentUserModel | null>(null);
  currentUser$ = this.currentUserSubject.asObservable();

  constructor(
    private http: HttpClient,
    private cookieService: CookieService
    ) { }

  getCurrentUser(): Observable<CurrentUserModel> {
    const token = this.cookieService.get('jwtToken');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<CurrentUserModel>(this.currentUserUrl, { headers }).pipe(
      tap(user => this.currentUserSubject.next(user)), //Update the BehaviorSubject with the new user data
      catchError((error) => {
        console.error("Error getting current user:", error);
        throw error;
      })
    );
  }

  setCurrentUser(user: CurrentUserModel | null): void {
    this.currentUserSubject.next(user);
  }
}
