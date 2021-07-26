import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { UserModel } from '../_models/user-model';
import { ReplaySubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseApiUrl = environment.baseApiUrl + 'account/';
  private currentUserSource = new ReplaySubject<UserModel>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  login(loginModel: any) {
    return this.http.post(this.baseApiUrl + 'login', loginModel).pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  setCurrentUser(user: UserModel) {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  loadCurrentUser() {
    const user = JSON.parse(localStorage.getItem('user'));
    if(user){
      this.currentUserSource.next(user);
    } else {
      this.currentUserSource.next(null);
    }
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.router.navigateByUrl("/");
  }

  navigateTo(path: string)
  {
    this.router.navigateByUrl(path);
  }
}
