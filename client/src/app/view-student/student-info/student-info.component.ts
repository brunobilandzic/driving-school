import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { StudentModel } from 'src/app/_models/student';
import { AddToGroupComponent } from './add-to-group/add-to-group.component';
import { SignToTestComponent } from './sign-to-test/sign-to-test.component';

@Component({
  selector: 'app-student-info',
  templateUrl: './student-info.component.html',
  styleUrls: ['./student-info.component.css']
})
export class StudentInfoComponent implements OnInit, OnChanges {
  @Input() student: StudentModel;
  @Output() refreshStudent = new EventEmitter<string>()
  @Input() loadStudent: (username: string) => void;
  groupModal: BsModalRef;
  testModal: BsModalRef;
  constructor(private modalService: BsModalService) { }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    //Called before any other lifecycle hook. Use it to inject dependencies, but avoid any serious work here.
    //Add '${implements OnChanges}' to the class.
    console.log(changes);
    
  }


  getLecturesCount(){
    return this.student.lectures.filter(l => l.attendance).length;
  }

  getTestsCount() {
    return this.student.regulationsTests.filter(rt => new Date(rt.regulationsTestDate).getTime() < Date.now() && rt.score < 90).length;
  }

  openGroupModal() {
    const initialState = {student: this.student, refreshStudent: this.refreshStudent};
    this.groupModal = this.modalService.show(AddToGroupComponent, {initialState});
  }

  openTestModal() {
    const initialState = {student: this.student, refreshStudent: this.refreshStudent};
    this.testModal = this.modalService.show(SignToTestComponent, {initialState});
  }
}
