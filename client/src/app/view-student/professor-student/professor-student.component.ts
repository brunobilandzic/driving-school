import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { passedToday } from 'src/app/_helpers/dates';
import { StudentModel } from 'src/app/_models/student';
import { UsernameToId } from 'src/app/_models/username-to-id';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-professor-student',
  templateUrl: './professor-student.component.html',
  styleUrls: ['./professor-student.component.css'],
})
export class ProfessorStudentComponent implements OnInit {
  @Output() toggleView = new EventEmitter<boolean>();
  @Input() isInstructor: boolean;
  @Input() username: string;
  student: StudentModel;
  constructor(private membersService: MembersService) {}

  ngOnInit(): void {
    this.loadStudent(this.username);
  }

  toggleViewClick() {
    this.toggleView.emit(true);
  }

  loadStudent(username: string) {
    this.membersService.student = null;
    this.membersService.getStudent(username).subscribe((student) => {
      this.student = student;
      console.log(this.student);
    });
  }
  checkTestDate(dateString: string) {
    return !passedToday(dateString);
  }

  deleteFromTest(event: any, regulationsTestId: number) {
    console.log(event.target)
    if (
      window.confirm(
        'Are you sure you want to delete student from this test?'
      ) == false
    )
      return;
    let studentFromTest: UsernameToId = {
      username: this.student.username,
      id: regulationsTestId,
    };
    this.membersService
      .deleteFromTest(studentFromTest)
      .subscribe(
        () =>
          (this.student.regulationsTests = this.student.regulationsTests.filter(
            (rt) => rt.regulationsTestId != regulationsTestId
          ))
      );
  }
}
