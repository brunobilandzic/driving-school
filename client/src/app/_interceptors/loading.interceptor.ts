import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpResponse,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { delay, finalize, map } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private spinner: NgxSpinnerService) {}

  // Artificially delaying the request 
  // So we have a 'feeling' of loading in development.
  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    /*
    this.spinner.show();
    this.spinner.hide();
    return next.handle(request).pipe(
      map((httpEvent: HttpEvent<any>) => {
        if (httpEvent instanceof HttpResponse) {
          this.spinner.hide();
        }
        return httpEvent;
      })
    );
    */
      
    this.spinner.show();
    return next.handle(request)
      .pipe(
        delay(1000),
        finalize(() => {
          this.spinner.hide();
        })
        
      );

  }
}
