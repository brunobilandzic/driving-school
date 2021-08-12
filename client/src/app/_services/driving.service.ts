import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { getKey } from '../_helpers/paginationToKey';
import { DrivingSessionModel } from '../_models/driving-session';
import { PaginatedResult } from '../_models/pagination';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class DrivingService {
  baseUrl = environment.baseApiUrl;
  drivingSessions = new Map();
  constructor(
    private http: HttpClient
  ) { }

  getDrivingSessions(pageNumber: number, pageSize: number) {
    let key = getKey(pageNumber, pageSize);
    let cachedSessions = this.drivingSessions.get(key);
    if(cachedSessions != null) return of(cachedSessions);

    let params = getPaginationHeaders(pageNumber, pageSize);
    return getPaginatedResult<DrivingSessionModel []>(this.baseUrl + 'instructor/sessions', params, this.http)
      .pipe(map((paginatedResult: PaginatedResult<DrivingSessionModel[]>) => {
        this.drivingSessions.set(key, paginatedResult);
        return paginatedResult;
      }))
  }

  createDrivingSession(drivingSession: any){
    return this.http.post(this.baseUrl + 'instructor/sessions', drivingSession);
  }

  updateDrivingSession(drivingSession: any){
    return this.http.put(this.baseUrl + 'instructor/session-general', drivingSession)
  }

  putInstructorRemarks(instructorRemarks: any){
    return this.http.put(this.baseUrl + 'instructor/sessions', instructorRemarks);
  }
}
