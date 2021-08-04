import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UsernameToBool } from 'src/app/_models/username-to-bool';
import { UsernameToId } from 'src/app/_models/username-to-id';
import { UsernamesToId } from 'src/app/_models/usernames-to-id';
import { LecturesService } from 'src/app/_services/lectures.service';

@Component({
  selector: 'app-lecture-attendance',
  templateUrl: './lecture-attendance.component.html',
  styleUrls: ['./lecture-attendance.component.css'],
})
export class LectureAttendanceComponent implements OnInit {
  attendances: UsernameToBool[];
  lectureId: string;
  studentsToAttend: string[] = [];
  constructor(
    private route: ActivatedRoute,
    private lecturesService: LecturesService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.route.data.subscribe((data) => {
      this.attendances = data.attendances;
      this.lectureId = data.id;
    });
  }

  toggleAttendance(username: string, thruth: boolean) {
    let message = 'Set to ' + (thruth ? 'false' : 'true') + '?';

    if (window.confirm(message)) {
      let usernameToId = new UsernameToId();
      usernameToId.username = username;
      usernameToId.id = parseInt(this.lectureId);
      this.lecturesService.toggleAttendance(usernameToId).subscribe(() => {
        let a = this.attendances.find((elem) => elem.username == username);
        if (!a) return;
        a.thruth = !a.thruth;
      });
    }
  }

  attendStudent(e: any) {
    let username: string = e.target.value;

    if (this.studentsToAttend.includes(username))
      return (this.studentsToAttend = this.studentsToAttend.filter(
        (u) => u != username
      ));

    this.studentsToAttend.push(username);
  }

  selectAll() {
    this.studentsToAttend = this.attendances
      .filter((at) => !at.thruth)
      .map((at) => at.username);
  }

  loadAttendances()
  {
    this.lecturesService.getAttendance(this.lectureId)
      .subscribe(attendances => this.attendances = attendances);
  }

  submit() {
    let usernamesToId = new UsernamesToId();

    usernamesToId.usernames = this.studentsToAttend;
    usernamesToId.id = parseInt(this.lectureId);

    this.lecturesService.markAttendances(usernamesToId).subscribe(() => {
      this.loadAttendances();
      this.studentsToAttend = [];
      this.toastr.success("Successfully added students.")
    });
  }
}
