import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router'
import {MatDrawer} from '@angular/material/sidenav';
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

  constructor(
    private router: Router,
    private userService: UserService
  ) { }

  initCurrentUser(): void {
    this.userService.getCurrentUser().subscribe(
      (user: CurrentUserModel) => {
        this.currentUser = user;
        console.log(this.currentUser)
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
