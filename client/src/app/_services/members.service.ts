import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, ReplaySubject } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../_models/pagination';
import { UserModel } from '../_models/user';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.baseApiUrl ;
  users: UserModel[] = [];
  students = new Map();
  constructor(private http: HttpClient) {}

  getAll() {
    if (this.users.length == 0) {
      this.http
        .get(this.baseUrl+ 'users/')
        .subscribe((users: UserModel[]) => (this.users = users));
    } else {
      return of(this.users);
    }
  }

  getRegulationsGroups()
  {
    return this.http
      .get(this.baseUrl + 'professor/regulations-groups')
      
  }

  getStudents(view: string, pageNumber: number, pageSize: number)
  {
    console.log(view, pageNumber, pageSize)
    let key = view + '-' + pageNumber.toString() + '-' + pageSize.toString();
    let students = this.students.get(key);
    
    if(students != undefined) return of(students);
    
    let params = getPaginationHeaders(pageNumber, pageSize);


    return getPaginatedResult<UserModel []>(this.baseUrl + view + '/students', params, this.http)
      .pipe(map((result: PaginatedResult<UserModel []>) => {
        this.students.set(key, result);
        return result;
      }))
    
  }
}
