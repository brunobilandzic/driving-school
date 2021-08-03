import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Lecture } from '../_models/lecture';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class LecturesService {
  baseUrl = environment.baseApiUrl + 'professor/';
  lectures = new Map();
  constructor(private http: HttpClient) {}

  getLectures(pageNumber: number, pageSize: number) {
    let key = pageNumber.toString() + '-' + pageSize.toString();
    let lecturesCache = this.lectures.get(key);
    if (lecturesCache) return of(this.lectures.get(key));

    let params = getPaginationHeaders(pageNumber, pageSize);

    return getPaginatedResult<Lecture[]>(
      this.baseUrl + 'lectures',
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
      .reduce((arr, elem) => arr.push([...elem]), [])
      .filter((l: Lecture) => l.lectureId == parseInt(lectureId))[0];

    if (lecture) return of(lecture);

    return this.http.get<Lecture>(this.baseUrl + 'lectures/' + lectureId);
  }
}
