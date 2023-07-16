import { Component, OnInit, ViewChild } from '@angular/core';
import {Router} from '@angular/router'
import {MatDrawer, MatSidenav} from '@angular/material/sidenav';
//Services
import { UserService } from '../shared/services/user-service/user.service';
//Models
import { CurrentUserModel } from '../shared/models/currentuser-model';

//Components

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  currentUser: CurrentUserModel;
  opened: boolean;

  @ViewChild('sidenav') sidenav: MatSidenav;

  events: string[] = [];

  shouldRun = [/(^|\.)plnkr\.co$/, /(^|\.)stackblitz\.io$/].some(h => h.test(window.location.host));

  constructor(
    private router: Router,
    private userService: UserService
  ) { }

  toggleSidenav() {
    this.sidenav.toggle();
  }

  initCurrentUser(): void {
    this.userService.getCurrentUser().subscribe(
      (user: CurrentUserModel) => {
        this.currentUser = user;
        console.log(this.currentUser)
        var cookies = document.cookie;
        console.log(cookies);
      },
      (error) => {
        console.error(error);
      }
    )
  }
  ngOnInit(): void {
    this.initCurrentUser();
  }

}
