import { Component, OnInit } from '@angular/core';
import { AppUser } from './ap-user';
import { AppUserAuth } from './app-user-auth';
import { SecurityService } from './security.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'ptc-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user: AppUser = new AppUser();
  securityObject: AppUserAuth = null;
  returnUrl = '';

  constructor(private securityService: SecurityService,
              private route: ActivatedRoute,
              private router: Router) { }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
  }

  login() {
    this.securityService.login(this.user).subscribe(
      resp => {
                this.securityObject = resp;
                if (this.returnUrl) {
                  this.router.navigateByUrl(this.returnUrl);
                }
              },
              // If the API return and error or 404...
              () => {
                // Initialize security object to display error message
                this.securityObject  = new AppUserAuth();
              }
    );
  }
}