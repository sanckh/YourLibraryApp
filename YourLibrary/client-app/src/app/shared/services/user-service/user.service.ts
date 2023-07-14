import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  // private userUrl = 'https://your-api-url/user'; // Replace with the actual API endpoint

  // constructor(private http: HttpClient) {}

  // getUserData(): Observable<any> {
  //   // Make an HTTP request to fetch user data
  //   return this.http.get<any>(this.apiUrl);
  // }
}
