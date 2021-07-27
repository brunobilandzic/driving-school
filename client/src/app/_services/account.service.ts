import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { map, take } from 'rxjs/operators';
import { UserModel } from '../_models/user';
import { ReplaySubject } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseApiUrl = environment.baseApiUrl + 'account/';
  private currentUserSource = new ReplaySubject<UserModel>(1);
  currentUser$ = this.currentUserSource.asObservable();
  userRoles: string [] =  [];

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
    user.roles = [];
    let roles: any = this.getDecodedToken(user.token).role;

    if(!Array.isArray(roles))
      user.roles.push(roles);
    else
      user.roles = [...roles]

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

  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split(".")[1]));
  }

  navigateTo(path: string)
  {
    this.router.navigateByUrl(path);
  }
}
