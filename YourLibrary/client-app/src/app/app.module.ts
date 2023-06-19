import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
/*import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';*/


//Components
import { AppComponent } from './app.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component';
import { SweetAlertService } from './shared/services/sweetalert.service';
import { RegistrationModalComponent } from './shared/components/registration-modal/registration-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    LoginComponent,
    RegistrationModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    //SweetAlert2Module.forRoot(),
    //SweetAlert2Module.forChild(),
    ReactiveFormsModule,
  ],
  providers: [
    SweetAlertService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
