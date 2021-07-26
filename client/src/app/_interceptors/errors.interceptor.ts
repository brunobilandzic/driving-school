import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorsInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if(error) {
            switch (error.status) {
              case 401:
                this.toastr.error("Unauthorized");
                break;
              case 400:
                this.toastr.error("Bad Request");
                break;
              case 404:
                this.toastr.error("Not found");
                break;
              case 500:
                this.toastr.error("Internal server error");
                break;
              default:
                this.toastr.error("Something went wrong");
                break;
            }
          }
          return throwError(error);

        })
      )
      
      
      
  }
}
