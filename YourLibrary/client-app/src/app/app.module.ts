import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { MatSidenavModule } from '@angular/material/sidenav'
import { MatListModule } from '@angular/material/list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { CookieService } from 'ngx-cookie-service';

/*import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';*/


//Components
import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component';
import { SweetAlertService } from './shared/services/sweetalert.service';
import { RegistrationModalComponent } from './shared/components/registration-modal/registration-modal.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    LoginComponent,
    RegistrationModalComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    //SweetAlert2Module.forRoot(),
    //SweetAlert2Module.forChild(),
    ReactiveFormsModule,
    NoopAnimationsModule,
    MatSidenavModule,
    MatListModule,
    MatToolbarModule,
    MatIconModule,
  ],
  providers: [
    SweetAlertService,
    CookieService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
