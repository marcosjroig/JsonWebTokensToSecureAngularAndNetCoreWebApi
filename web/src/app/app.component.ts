import { Component } from '@angular/core';
import { AppUserAuth } from './security/app-user-auth';
import { SecurityService } from './security/security.service';

@Component({
  selector: 'ptc-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  securityObject: AppUserAuth = null;
  title = 'Angular with Json Web Token';

  constructor(private securityService: SecurityService) {
      this.securityObject = this.securityService.securityObject;
   }

   logout() {
     this.securityService.logout();
   }
}
