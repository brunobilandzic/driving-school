import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Lecture } from '../_models/lecture';
import { LecturesService } from '../_services/lectures.service';

@Injectable({
  providedIn: 'root'
})
export class LectureResolver implements Resolve<Lecture> {
  constructor(private lecturesService: LecturesService) {

  }
  resolve(route: ActivatedRouteSnapshot): Observable<Lecture> {
    return this.lecturesService.getLecture(route.paramMap.get("lectureId"));
  }
}
