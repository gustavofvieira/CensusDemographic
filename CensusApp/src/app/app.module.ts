import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';


//component
import { LoginComponent } from './components/user/login/login.component';
import { PersonComponent } from './components/person/person.component';
import { CreateUserComponent } from './components/user/create/create.component';
import { HomeComponent } from './components/home/home.component';
// import { CensusComponent } from './components/census/census.component';



//services
import { UserService } from 'src/app/services/user/user.service';
import { PersonService } from 'src/app/services/person/person.service';
import { LocalStorageService } from './services/local-storage.service';

import { CommonModule } from '@angular/common';

import { HttpClientModule } from "@angular/common/http";
import { ReactiveFormsModule } from "@angular/forms";
import { ModalModule } from "ngx-bootstrap/modal";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    LoginComponent,
    PersonComponent,
    CreateUserComponent,
    HomeComponent,
    // CensusComponent,
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ModalModule.forRoot()
  ],
  providers: [HttpClientModule,UserService,PersonService,LocalStorageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
