import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentModel } from 'src/app/_models/student';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-professor-student',
  templateUrl: './professor-student.component.html',
  styleUrls: ['./professor-student.component.css']
})
export class ProfessorStudentComponent implements OnInit {
  @Output() toggleView = new EventEmitter<boolean>();
  @Input() isInstructor: boolean;
  @Input() username: string;
  student: StudentModel;
  constructor(private membersService: MembersService) { }

  ngOnInit(): void {
    this.loadStudent(this.username);
  }

  toggleViewClick() {
    this.toggleView.emit(true);
  }

  loadStudent(username: string) {
    this.membersService.student = null;
    this.membersService.getStudent(username)
      .subscribe((student) => {
        this.student = student
        console.log(this.student);
      })
  }

}
