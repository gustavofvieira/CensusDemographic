import { Component, OnInit, Inject, LOCALE_ID  } from '@angular/core';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { Router } from '@angular/router';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit{

  constructor(@Inject(LOCALE_ID) public locale: string,private userService: UserService, private localStorageService: LocalStorageService,
  private router: Router) {}

  welcome: string | undefined;

  ngOnInit(): void {
    this.userService.Authenticated().subscribe((result) => {
      this.welcome = result
    });
  }

  SignOut(): void {
    this.localStorageService.remove("token");
    this.router.navigate(['/']);
  }
}

