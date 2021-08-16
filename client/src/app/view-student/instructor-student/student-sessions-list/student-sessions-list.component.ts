import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-student-sessions-list',
  templateUrl: './student-sessions-list.component.html',
  styleUrls: ['./student-sessions-list.component.css']
})
export class StudentSessionsListComponent implements OnInit {
  @Input() drivingSessionsTaken;
  role="Instructor";
  constructor() { }

  ngOnInit(): void {
  }

}
