import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginComponent } from './login/login.component'
import { SearchComponent } from './search/search.component'
import { AuthGuard } from './shared/services/authguard.service'

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' }, //empty path represents the base URL of the app
  { path: 'login', component: LoginComponent }, 
  { path: 'home', component: HomePageComponent, canActivate: [AuthGuard], children: [
    // { path: '', redirectTo: 'profile', pathMatch: 'full' },
  ]
  },
  { path: 'search', component: SearchComponent },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
