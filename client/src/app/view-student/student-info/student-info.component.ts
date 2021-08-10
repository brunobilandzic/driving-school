import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  SimpleChanges,
} from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { StudentModel } from 'src/app/_models/student';
import { AddToGroupComponent } from './add-to-group/add-to-group.component';
import { SignToTestComponent } from './sign-to-test/sign-to-test.component';

@Component({
  selector: 'app-student-info',
  templateUrl: './student-info.component.html',
  styleUrls: ['./student-info.component.css'],
})
export class StudentInfoComponent implements OnInit {
  @Input() student: StudentModel;
  @Output() refreshStudent = new EventEmitter<string>();

  groupModal: BsModalRef;
  testModal: BsModalRef;
  constructor(private modalService: BsModalService) {}

  ngOnInit(): void {}

  getLecturesCount() {
    return this.student.lectures.filter((l) => l.attendance).length;
  }

  getUnattendedLecturesCount() {
    return this.student.lectures.filter((l) => !l.attendance && new Date(l.dateStart).getTime() < Date.now()).length;
  }

  getTestsCount() {
    return this.student.regulationsTests.filter(
      (rt) =>
        new Date(rt.regulationsTestDate).getTime() < Date.now() && rt.score < 90
    ).length;
  }

  openGroupModal() {
    const initialState = {
      student: this.student,
      refreshStudent: this.refreshStudent,
    };
    this.groupModal = this.modalService.show(AddToGroupComponent, {
      initialState,
    });
  }

  openTestModal() {
    const initialState = {
      student: this.student,
      refreshStudent: this.refreshStudent,
    };
    this.testModal = this.modalService.show(SignToTestComponent, {
      initialState,
    });
  }

  getStudentTest() {
    return this.student.regulationsTests.filter(
      (rt) => new Date(rt.regulationsTestDate).getTime() > Date.now()
    );
  }
}
