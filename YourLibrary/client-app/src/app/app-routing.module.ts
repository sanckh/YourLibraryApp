import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component'
import { MylibraryComponent } from './mylibrary/mylibrary.component';
import { AuthGuard } from './shared/services/authguard.service'

const routes: Routes = [
  { path: 'login', component: LoginComponent }, 
  { path: 'home', component: HomePageComponent, canActivate: [AuthGuard], children: [
    // { path: '', redirectTo: 'profile', pathMatch: 'full' },
  ]
  },
  { path: 'mylibrary', component: MylibraryComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: 'login', pathMatch: 'full' }, //empty path represents the base URL of the app

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
