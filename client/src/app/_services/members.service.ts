import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UserModel } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.baseApiUrl ;
  users: UserModel[] = [];
  constructor(private http: HttpClient) {}

  getAll() {
    if (this.users.length == 0) {
      console.log("sending");
      
      this.http
        .get(this.baseUrl+ 'users/')
        .subscribe((users: UserModel[]) => (this.users = users));
    }
  }

  getRegulationsGroups()
  {
    return this.http
      .get(this.baseUrl + 'professor/regulations-groups')
      
  }
}
