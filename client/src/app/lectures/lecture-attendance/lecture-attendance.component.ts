import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UsernameToBool } from 'src/app/_models/username-to-bool';
import { UsernameToId } from 'src/app/_models/username-to-id';
import { LecturesService } from 'src/app/_services/lectures.service';

@Component({
  selector: 'app-lecture-attendance',
  templateUrl: './lecture-attendance.component.html',
  styleUrls: ['./lecture-attendance.component.css']
})
export class LectureAttendanceComponent implements OnInit {
  attendances: UsernameToBool[];
  lectureId: string; 
  constructor(private route: ActivatedRoute, private lecturesService: LecturesService) { }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.attendances = data.attendances
      this.lectureId = data.id
    });
  }

  toggleAttendance(username: string, thruth: boolean)
  {
    let message = "Set to " + (thruth ? "false" : "true") + "?";

    if(window.confirm(message))
    {
      let usernameToId = new UsernameToId();
      usernameToId.username = username;
      usernameToId.id = parseInt(this.lectureId);
      this.lecturesService.toggleAttendance(usernameToId)
        .subscribe(() => {
          let a = this.attendances.find(elem => elem.username == username);
          if(!a) return;
          a.thruth = !a.thruth;
        })
    }
  }

}
