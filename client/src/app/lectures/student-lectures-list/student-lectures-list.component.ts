import { Component, OnInit } from '@angular/core';
import { passedToday } from 'src/app/_helpers/dates';
import { StudentLectureModel } from 'src/app/_models/student-lecture';
import { LecturesService } from 'src/app/_services/lectures.service';

@Component({
  selector: 'app-student-lectures-list',
  templateUrl: './student-lectures-list.component.html',
  styleUrls: ['./student-lectures-list.component.css']
})
export class StudentLecturesListComponent implements OnInit {
  studentLectures: StudentLectureModel[];
  constructor(
    private lecturesService: LecturesService
  ) { }

  ngOnInit(): void {
    this.loadLectures();
  }

  loadLectures() {
    this.lecturesService.getStudentLectures()
      .subscribe((ls: StudentLectureModel []) => this.studentLectures = ls)
  }

  checkFuture(dateString: string) {
    return passedToday(dateString);
  }

}
