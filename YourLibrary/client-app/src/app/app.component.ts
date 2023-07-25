import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router'
import { MatDrawer, MatSidenav } from '@angular/material/sidenav';
import { Location } from '@angular/common';
//Services
import { UserService } from './shared/services/user-service/user.service';
import { AuthService } from './shared/services/auth.service'
//Models
import { CurrentUserModel } from './shared/models/currentuser-model';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  activeLink: string;
  currentUser: CurrentUserModel;
  searchForm = new FormGroup({
    searchTerm: new FormControl('')
  });

  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(
    private router: Router,
    private userService: UserService,
    private authService: AuthService,
    private location: Location,
  ) { }

  toggleSidenav() {
    this.sidenav.toggle();
  }

  initCurrentUser(): void {
    this.userService.getCurrentUser().subscribe(
      (user: CurrentUserModel) => {
        this.currentUser = user;
      },
      (error) => {
        console.error(error);
      }
    )
  }

  isLoginPage(): boolean {
    const currentRoute = this.location.path();
    return currentRoute.includes('/login');
  }

  sideNavOnLogoutClicked(): void {
    this.authService.logout();
  }

  sideNavOnSearchClicked(): void {
    this.activeLink = 'search';
    this.router.navigate(['/search']);
  }

  sideNavOnHomeClicked(): void {
    this.activeLink = 'home';
    this.router.navigate(['/home']);
  }

  navigateToSearchPage(): void {
    this.activeLink = 'search';
    const searchTerm = this.searchForm.get('searchTerm')!.value;
    if (searchTerm && searchTerm.trim() !== '') {
      this.router.navigate(['/search', { query: searchTerm }]);
    }
  }

  ngOnInit(): void {
    this.initCurrentUser();
  }
}
