import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DriverModel } from 'src/app/_models/driver';
import { transpileModule } from 'typescript';

@Component({
  selector: 'app-instructor-student-info',
  templateUrl: './instructor-student-info.component.html',
  styleUrls: ['./instructor-student-info.component.css'],
})
export class InstructorStudentInfoComponent implements OnInit {
  @Input() driver: DriverModel;
  constructor(private router: Router) {}

  ngOnInit(): void {}

  getDrivenSessionsCount() {
    return this.driver.drivingSessionsTaken.filter((ds) => ds.isDriven).length;
  }

  openSessionForm() {
    this.router.navigate([
      '/sessions/add',
      {
        data: JSON.stringify({ driverUsername: this.driver.username }),
        createForDriver: true,
      },
    ]);
  }
}
