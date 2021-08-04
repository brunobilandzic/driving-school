import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { UsernameToBool } from '../_models/username-to-bool';
import { LecturesService } from '../_services/lectures.service';

@Injectable({
  providedIn: 'root'
})
export class AttendanceResolver implements Resolve<UsernameToBool []> {

  constructor(private lecturesService: LecturesService) {
    
  }
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<UsernameToBool []> {
    return this.lecturesService.getAttendance(route.paramMap.get("id"));
  }
}
