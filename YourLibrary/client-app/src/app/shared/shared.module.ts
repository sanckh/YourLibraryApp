import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationModalComponent } from './components/registration-modal/registration-modal.component';
import { BookListComponent } from './components/google-books/book-list/book-list.component';
import { GoogleBookListComponent } from './components/google-book-list/google-book-list.component';



@NgModule({
  declarations: [
    //RegistrationModalComponent
  
    BookListComponent,
    GoogleBookListComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
