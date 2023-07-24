import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router'
import { MatDrawer, MatSidenav } from '@angular/material/sidenav';
//Services
import { UserService } from './shared/services/user-service/user.service';
import { AuthService } from './shared/services/auth.service'
//Models
import { CurrentUserModel } from './shared/models/currentuser-model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  currentUser: CurrentUserModel;

  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(
    private router: Router,
    private userService: UserService,
    private authService: AuthService,
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

  sideNavOnLogoutClicked(): void {
    this.authService.logout();
  }

  sideNavOnSearchClicked(): void {
    this.router.navigate(['/search']);
  }

  ngOnInit(): void {
    this.initCurrentUser();
  }
}
