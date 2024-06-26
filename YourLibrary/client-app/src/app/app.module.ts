import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { CookieService } from 'ngx-cookie-service';

//Material
import { MatSidenavModule } from '@angular/material/sidenav'
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatMenuModule } from '@angular/material/menu';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

//Components
import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component';
import { SweetAlertService } from './shared/services/sweetalert.service';
import { RegistrationModalComponent } from './shared/components/registration-modal/registration-modal.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { GoogleBookListComponent } from './shared/components/google-book-list/google-book-list.component';
import { SearchModalComponent } from './shared/components/search-modal/search-modal.component';
import { MylibraryComponent } from './mylibrary/mylibrary.component'

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    LoginComponent,
    RegistrationModalComponent,
    GoogleBookListComponent,
    SearchModalComponent,
    MylibraryComponent
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    NoopAnimationsModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    MatExpansionModule,
    MatTooltipModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  providers: [
    SweetAlertService,
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
