import { Component, OnInit, ViewChild } from '@angular/core';
import {Router} from '@angular/router'
import {MatDrawer, MatSidenav} from '@angular/material/sidenav';
//Services
import { UserService } from '../shared/services/user-service/user.service';
import { BookService } from '../shared/services/book-service/book-service.service';
import { AuthService } from '../shared/services/auth.service'
//Models
import { CurrentUserModel } from '../shared/models/currentuser-model';
import { BookModel } from '../shared/models/book-model';

//Components

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {

  currentUser: CurrentUserModel;
  opened: boolean;

  //Placeholder:
  //recentlyAddedBooks: BookModel[] = [];
  recentlyAddedBooks: any[] = [];
  userLists: any[] = [];
  recommendedBooks: any[] = [];


  @ViewChild('sidenav') sidenav: MatSidenav;

  events: string[] = [];

  constructor(
    private router: Router,
    private userService: UserService,
    private bookService: BookService,
    private authService: AuthService,
  ) { }

//PlaceHolderData!


  initRecentlyAddedBooks(): void {
    // Get the recently added books from your service here
    // Placeholder data for demonstration
    this.recentlyAddedBooks = [
      { title: 'Book 1', description: 'Description 1' },
      { title: 'Book 2', description: 'Description 2' },
      { title: 'Book 3', description: 'Description 3' }
    ];
  }

  initUserLists(): void {
    // Get the user's lists from your service here
    // Placeholder data for demonstration
    this.userLists = [
      { title: 'List 1', description: 'Description 1' },
      { title: 'List 2', description: 'Description 2' },
      { title: 'List 3', description: 'Description 3' }
    ];
  }

  initRecommendedBooks(): void {
    // Get the recommended books from your service here
    // Placeholder data for demonstration
    this.recommendedBooks = [
      { title: 'Book 4', description: 'Description 4' },
      { title: 'Book 5', description: 'Description 5' },
      { title: 'Book 6', description: 'Description 6' }
    ];
  }

  toggleSidenav() {
    this.sidenav.toggle();
  }



  //initRecentlyAddedBooks(): void {
  //  this.bookService.getRecentlyAddedBooks().subscribe(
  //    (books: BookModel[]) => {
  //      this.recentlyAddedBooks = books;
  //    },
  //    (error) => {
  //      console.error('Failed to get recently added books: ', error);
  //  }
  //  );
  //}

  ngOnInit(): void {
    this.initRecentlyAddedBooks();
    this.initUserLists();
    this.initRecommendedBooks();
  }

}
