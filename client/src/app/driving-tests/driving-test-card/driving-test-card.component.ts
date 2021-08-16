import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { passedToday } from 'src/app/_helpers/dates';
import { DrivingTestModel } from 'src/app/_models/driving-test';
import { ExamineTestModalComponent } from '../examine-test-modal/examine-test-modal.component';

@Component({
  selector: 'app-driving-test-card',
  templateUrl: './driving-test-card.component.html',
  styleUrls: ['./driving-test-card.component.css'],
})
export class DrivingTestCardComponent implements OnInit {
  @Input() drivingTest: DrivingTestModel;
  @Input() role: string;
  @Output() deleteTest = new EventEmitter<number>();
  @Output() invokeRefresh = new EventEmitter<boolean>();
  examineModalRef: BsModalRef;
  constructor(private modalService: BsModalService, private router: Router) {}

  ngOnInit(): void {}

  onDeleteTest() {
    if (
      window.confirm(
        'Are you sure you want to delete test ' +
          this.drivingTest.drivingSessionId
      ) == false
    )
      return;
    this.deleteTest.emit(this.drivingTest.drivingSessionId);
  }

  onExamineTestClick() {
    const initialState = {
      drivingTest: this.drivingTest,
      invokeRefresh: this.invokeRefresh,
    };
    this.examineModalRef = this.modalService.show(ExamineTestModalComponent, {
      initialState,
    });
  }

  checkTestDate(dateString) {
    return passedToday(dateString);
  }

  onEditTest() {
    this.router.navigate([
      '/instructor/sessions/add',
      {
        data: JSON.stringify(this.drivingTest.drivingSession),
        examinerEdit: true,
      },
    ]);
  }
}
