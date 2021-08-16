import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Lecture } from '../_models/lecture';
import { UsernameToBool } from '../_models/username-to-bool';
import { UsernameToId } from '../_models/username-to-id';
import { UsernamesToId } from '../_models/usernames-to-id';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class LecturesService {
  baseUrl = environment.baseApiUrl;
  lectures = new Map();
  constructor(private http: HttpClient) {}

  getLectures(pageNumber: number, pageSize: number) {
    let key = pageNumber.toString() + '-' + pageSize.toString();
    let lecturesCache = this.lectures.get(key);
    if (lecturesCache) return of(this.lectures.get(key));

    let params = getPaginationHeaders(pageNumber, pageSize);

    return getPaginatedResult<Lecture[]>(
      this.baseUrl + 'professor/lectures',
      params,
      this.http
    ).pipe(
      map((response) => {
        this.lectures.set(key, response);
        return response;
      })
    );
  }

  getLecture(lectureId: string): Observable<Lecture> {
    let lecture = [...this.lectures.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((l: Lecture) => l.lectureId == parseInt(lectureId));
    if (lecture) return of(lecture);

    return this.http.get<Lecture>(this.baseUrl + 'professor/lectures/' + lectureId);
  }

  getAttendance(lectureId: string): Observable<UsernameToBool []> {
    return this.http.get<UsernameToBool []>(this.baseUrl + 'professor/attendances/' + lectureId);
  }

  toggleAttendance(usernameToId: UsernameToId): Observable<boolean> {
    return this.http.post(this.baseUrl + 'professor/attendances-toggle', usernameToId).pipe(map(
      () => {return true;}
    ))
  }

  markAttendances(usernamesToId: UsernamesToId): Observable<boolean> {
    return this.http.post(this.baseUrl + 'professor/attendances', usernamesToId).pipe(map(
      () => {return true;}
    ))
  }

  getStudentLectures() {
    return this.http.get(this.baseUrl + 'student/lectures');
  }

  getLectureTopics() {
    return this.http.get(this.baseUrl + 'common/lecture-topics')
  }
}
