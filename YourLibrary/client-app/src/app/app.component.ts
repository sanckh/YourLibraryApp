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
import { filter } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  activeLink: string;
  currentUser: CurrentUserModel | null = null;
  searchForm = new FormGroup({
    searchTerm: new FormControl('')
  });

  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(
    private router: Router,
    private userService: UserService,
    public authService: AuthService,
    private location: Location,
  )
  {
    // Subscribe to the currentUser$ observable to update the currentUser property whenever it changes
    this.userService.currentUser$.subscribe(user => this.currentUser = user);
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
    this.userService.currentUser$
      .pipe(filter(user => !!user))
      .subscribe(user => {
        this.currentUser = user;
      });

    if (this.authService.isAuthenticated()) {
      this.userService.getCurrentUser();
    }
  }


}
