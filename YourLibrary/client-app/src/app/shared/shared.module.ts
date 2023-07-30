import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegistrationModalComponent } from './components/registration-modal/registration-modal.component';
import { GoogleBookListComponent } from './components/google-book-list/google-book-list.component';
import { SearchModalComponent } from './components/search-modal/search-modal.component';



@NgModule({
  declarations: [
    //RegistrationModalComponent
  
    GoogleBookListComponent,
    SearchModalComponent
  ],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
