import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/user/login/login.component';
import { PersonComponent } from './components/person/person.component';
import { CreateUserComponent } from './components/user/create/create.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'user/create', component: CreateUserComponent },
  { path: 'person', component: PersonComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
