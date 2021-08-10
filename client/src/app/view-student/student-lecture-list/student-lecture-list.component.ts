import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { StudentModel } from 'src/app/_models/student';
import { StudentLectureModel } from 'src/app/_models/student-lecture';
import { UsernameToId } from 'src/app/_models/username-to-id';
import { LecturesService } from 'src/app/_services/lectures.service';

@Component({
  selector: 'app-student-lecture-list',
  templateUrl: './student-lecture-list.component.html',
  styleUrls: ['./student-lecture-list.component.css']
})
export class StudentLectureListComponent implements OnInit {
  @Input() lectures: StudentLectureModel[];
  constructor(
    private lecturesService: LecturesService,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {
    console.log(this.lectures);
  }

  getEditPromptText(lectureId: number) {
    let lecture = this.lectures.find(l => l.lectureId == lectureId);
    if(!lecture) return;
    
    return lecture.attendance ? "Mark unattended?" : "Mark attended?";
  }
  

  toggleAttendance(lectureId: number) {
    console.log(lectureId)
    console.log(this.lectures)
    let lecture = this.lectures.find(l => l.lectureId == lectureId);
    let useranmeToId: UsernameToId = {
      username: lecture.studentUsername,
      id: lectureId
    }

    if(window.confirm(this.getEditPromptText(lectureId)) == false) return;

    this.lecturesService.toggleAttendance(useranmeToId)
      .subscribe(() => {
        lecture.attendance = !lecture.attendance;
        this.toastr.success("Successfully toggled attendance.")
      })
  }

}
