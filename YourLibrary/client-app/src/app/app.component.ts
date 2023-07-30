import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router'
import { MatSidenav } from '@angular/material/sidenav';
import { Location } from '@angular/common';
//Services
import { UserService } from './shared/services/user-service/user.service';
import { AuthService } from './shared/services/auth.service'
//Models
import { CurrentUserModel } from './shared/models/currentuser-model';
import { filter } from 'rxjs';
import { GoogleBooksService } from './shared/services/google-books.service';
import { BookSearchResponseModel, Item } from './shared/models/booksearchresponsemodel';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  activeLink: string;
  currentUser: CurrentUserModel | null = null;
  searchTerm: string;
  isSearchModalOpen: boolean;
  searchResults: Item[];
  pageIndex: 0;
  pageSize: 10;

  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(
    private router: Router,
    private userService: UserService,
    public authService: AuthService,
    private location: Location,
    private googleBooksService: GoogleBooksService
  )
  {
    // Subscribe to the currentUser$ observable to update the currentUser property whenever it changes
    this.userService.currentUser$
      .subscribe(user =>
        this.currentUser = user);
  }

  toggleSidenav() {
    this.sidenav.toggle();
  }


  isLoginPage(): boolean {
    const currentRoute = this.location.path();
    return currentRoute.includes('/login');
  }

  sideNavOnLogoutClicked(): void {
    this.authService.logout();
  }

  sideNavOnHomeClicked(): void {
    this.activeLink = 'home';
    this.router.navigate(['/home']);
  }

  openSearchModal() {
    this.googleBooksService.getBooks(this.searchTerm, this.pageIndex * this.pageSize, this.pageSize)
      .subscribe((data: BookSearchResponseModel) => {
      if (data && data.items) {
        this.searchResults = data.items;
        this.isSearchModalOpen = true;
      } else {
        console.log('No items found in the response data');
      }
    }, error => {
      console.error('Error: ', error);
    });
  }

  onSubmit(searchTerm: string) {
    this.searchTerm = searchTerm;
    this.openSearchModal();
  }

  closeSearchModal(): void {
    this.isSearchModalOpen = false;
    this.searchResults = [];  // Clear the search results
    this.searchTerm = '';  // Clear the search term
  }

  ngOnInit(): void {
    this.userService.currentUser$
      .pipe(filter(user => !!user))
      .subscribe(user => {
        this.currentUser = user;
      });

    if (this.authService.isAuthenticated()) {
      this.userService.getCurrentUser().subscribe();
    }
  }


}
