import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.accountService.currentUser$.subscribe(user => {
      console.log(user);
      if(!user) return;
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${user.token}`
        }
      })
      
    });

    return next.handle(request);
    
  }
}
