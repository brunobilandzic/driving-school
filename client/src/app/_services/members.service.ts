import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of} from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DriverModel } from '../_models/driver';
import { PaginatedResult } from '../_models/pagination';
import { RegulationsGroup } from '../_models/regulations-group';
import { StudentModel } from '../_models/student';
import { UserModel } from '../_models/user';
import { UsernameToId } from '../_models/username-to-id';
import { UsernamesToId } from '../_models/usernames-to-id';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  baseUrl = environment.baseApiUrl ;
  users: UserModel[] = [];
  students = new Map();
  regulationsGroups = new Map();
  student: StudentModel;
  driver: DriverModel;
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
      .get(this.baseUrl + 'common/regulations-groups-active')
      
  }

  getAllRegulationsGroups(pageNumber: number, pageSize: number) {
    let key = pageNumber.toString() + '-' + pageSize.toString();
    let regulationsGroups = this.regulationsGroups.get(key);

    if(regulationsGroups != null) return of(regulationsGroups);

    let params = getPaginationHeaders(pageNumber, pageSize);

    return getPaginatedResult<RegulationsGroup []>(this.baseUrl + 'professor/regulations-groups', params, this.http)
      .pipe(map(rgs => {
        this.regulationsGroups.set(key, rgs);
        return rgs;
      }))

  } 



  getRegulationsTests()
  {
    return this.http
      .get(this.baseUrl + 'professor/regulations-tests')
  }

  getStudentsRegulationsTests(username: string)
  {
    return this.http
      .get(this.baseUrl + 'professor/regulations-tests/student/' + username)
  }

  getStudents(view: string, pageNumber: number, pageSize: number)
  {
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

  getAllStudents(regulationsGroupId = 0)
  {
    let params = new HttpParams();
    if(regulationsGroupId > 0) {
      // fetching students outside given group
      params = params.append("regulationsGroupId", regulationsGroupId.toString())
    }

    return this.http.get(this.baseUrl + 'users/all-students', {params});
  }

  getStudent(username: string): Observable<StudentModel> {
    if(this.student?.username == username) return of(this.student);

    return this.http.get(this.baseUrl + 'professor/students/' + username)
      .pipe(map((student: StudentModel) => {
        console.log(student);
        
        this.student = student;
        return student;
      }));

  }

  getDriver(username: string): Observable<DriverModel> {
    if(this.driver?.username == username) return of(this.driver);

    return this.http.get(this.baseUrl + 'instructor/students/' + username)
      .pipe(map((driver: DriverModel) => {
        console.log(driver);
        
        this.driver = driver;
        return driver;
      }));

  }

  getUsersFromRole(roleName: string) {
    return this.http.get(this.baseUrl + 'users/role/' + roleName);
  }

  createGroup(regulationsGroup: Partial<RegulationsGroup>) {
    return this.http.post(this.baseUrl + 'professor/regulations-groups', regulationsGroup); 
  }

  editGroup(regulationsGroup: any) {
    return this.http.put(this.baseUrl + 'professor/regulations-groups', regulationsGroup)
  }

  addToGroup(studentsToGroup: UsernamesToId ) {
    return this.http.post(this.baseUrl + 'professor/regulations-group-student', studentsToGroup);
  }

  signToTest(studentToTest: UsernameToId) {
    return this.http.post(this.baseUrl + 'professor/regulations-test-student', studentToTest)
  }

  deleteFromTest(studentFromTest: UsernameToId){
    return this.http.delete(this.baseUrl + 'professor/regulations-test-student', {body: studentFromTest});
  }
}
