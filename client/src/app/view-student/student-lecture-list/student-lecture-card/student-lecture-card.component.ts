import { Component, Input, OnInit } from '@angular/core';
import { StudentLectureModel } from 'src/app/_models/student-lecture';

@Component({
  selector: 'app-student-lecture-card',
  templateUrl: './student-lecture-card.component.html',
  styleUrls: ['./student-lecture-card.component.css']
})
export class StudentLectureCardComponent implements OnInit {
  @Input() lecture: StudentLectureModel;
  constructor() { }

  ngOnInit(): void {
  }

  lectureClass(): string {
    switch (this.lecture.attendance) {
      case true:
        return "attended"
      case false:
        return new Date(this.lecture.dateStart).getTime() > Date.now() ?  "pending" :  "not-attended"
      default:
        break;
    }
  }

}
