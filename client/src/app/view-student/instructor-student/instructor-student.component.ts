import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { sortByDateDescending } from 'src/app/_helpers/sortByDateStart';
import { DriverModel } from 'src/app/_models/driver';
import { StudentModel } from 'src/app/_models/student';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-instructor-student',
  templateUrl: './instructor-student.component.html',
  styleUrls: ['./instructor-student.component.css']
})
export class InstructorStudentComponent implements OnInit {
  @Output() toggleView = new EventEmitter<boolean>();
  @Input() isProfessor: boolean;
  @Input() username: string;
  driver: DriverModel;
  constructor(private membersService: MembersService) { }

  ngOnInit(): void {
    this.loadStudent();
  }

  toggleViewClick() {
    this.toggleView.emit(true);
  }

  loadStudent() {
    this.membersService.getDriver(this.username)
      .subscribe((driver) => {
        driver.drivingSessionsTaken = sortByDateDescending(driver.drivingSessionsTaken);
        this.driver = driver
      })
  }


}
