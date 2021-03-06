import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { getDateOnly } from '../_helpers/dates';
import { getKey } from '../_helpers/paginationToKey';
import { DrivingSessionModel } from '../_models/driving-session';
import { DrivingTestModel } from '../_models/driving-test';
import { PaginatedResult } from '../_models/pagination';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root',
})
export class DrivingService {
  baseUrl = environment.baseApiUrl;
  drivingSessions = new Map();
  drivingTests = new Map();
  constructor(private http: HttpClient) {}

  getDrivingSessions(role:string,  pageNumber: number, pageSize: number, sessionParams: any) {
    let key = getKey(pageNumber, pageSize, getDateOnly(sessionParams.dateStart), sessionParams.isDriven, sessionParams.sortDirection);
    let cachedSessions = this.drivingSessions.get(key);
    if (cachedSessions != null) return of(cachedSessions);

    let params = getPaginationHeaders(pageNumber, pageSize);
    
    if(sessionParams.dateStart != '') {
      let dateTemp = new Date(sessionParams.dateStart);
      // so its not 00:00 
      // then it messes the date when shifting timezones
      dateTemp.setHours(16);
      params = params.append("dateStart", dateTemp.toUTCString());
    }
      
    if(sessionParams.isDriven != null)
      params = params.append("isDriven", sessionParams.isDriven);

    if(sessionParams.sortDirection != null)
      params = params.append("sortDirection", sessionParams.sortDirection)

    return getPaginatedResult<DrivingSessionModel[]>(
      this.baseUrl + role + '/sessions',
      params,
      this.http
    ).pipe(
      map((paginatedResult: PaginatedResult<DrivingSessionModel[]>) => {
        this.drivingSessions.set(key, paginatedResult);
        return paginatedResult;
      })
    );
  }

  createDrivingSession(drivingSession: any) {
    return this.http.post(this.baseUrl + 'instructor/sessions', drivingSession);
  }

  updateDrivingSession(drivingSession: any) {
    return this.http.put(
      this.baseUrl + 'instructor/session-general',
      drivingSession
    );
  }

  updateDrivingSessionExaminer(drivingSession: any) {
    return this.http.put(
      this.baseUrl + 'examiner/session-general',
      drivingSession
    );
  }

  putRemarks(role: string, remarks: any) {
    console.log(remarks);
    
    return this.http.put(
      this.baseUrl + role + '/sessions',
      remarks
    );
  }

  getTestsFor(role: string, pageNumber: number, pageSize: number) {
    let key = role + '-' + getKey(pageNumber, pageSize);
    let cachedTests = this.drivingTests.get(key);

    if (cachedTests != null) return of(cachedTests);

    let params = null;
    if(role != 'Student')
      params = getPaginationHeaders(pageNumber, pageSize);

    return getPaginatedResult<DrivingTestModel[]>(
      this.baseUrl + role + '/tests',
      params,
      this.http
    ).pipe(
      map((paginatedResult: PaginatedResult<DrivingTestModel[]>) => {
        this.drivingTests.set(key, paginatedResult);
        return paginatedResult;
      })
    );
  }

  deleteSession(drivingSessionId) {
    return this.http.delete(this.baseUrl + 'instructor/sessions/' + drivingSessionId);
  }

  createTest(drivingSession: any) {
    return this.http.post(this.baseUrl + 'examiner/tests', drivingSession);
  }

  deleteTest(drivingSessionId: number) {
    return this.http.delete(this.baseUrl + 'examiner/tests/' + drivingSessionId);
  }

  examineTest(examination: any) {
    return this.http.post(this.baseUrl + 'examiner/examine', examination);
  }

}
