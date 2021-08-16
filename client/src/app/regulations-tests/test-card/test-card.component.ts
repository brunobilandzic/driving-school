import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { passedToday } from 'src/app/_helpers/dates';
import { RegulationsTestModel } from 'src/app/_models/regulations-test';
import { RegulationsTestsService } from 'src/app/_services/regulations-tests.service';
import { AddStudentsModalComponent } from './add-students-modal/add-students-modal.component';

@Component({
  selector: 'app-test-card',
  templateUrl: './test-card.component.html',
  styleUrls: ['./test-card.component.css'],
})
export class TestCardComponent implements OnInit {
  @Input() regTest: RegulationsTestModel;
  @Output() deleteTest = new EventEmitter<number>();
  @Output() updateTests = new EventEmitter<boolean>();
  addStudentsModal: BsModalRef;
  updatedTest: RegulationsTestModel;

  constructor(private router: Router, private modalService: BsModalService, private regTestService: RegulationsTestsService) {}

  ngOnInit(): void {
  }

  checkTestDate(dateString) {
    return passedToday(dateString);
  }

  onDeleteTest(testId: number) {
    if (
      window.confirm('Are you sure you want to delete test ' + testId) == false
    )
      return;

    this.deleteTest.emit(testId);
  }

  onEditTest(testId: number) {
    this.router.navigate([
      'regulations-tests/add-test',
      { data: JSON.stringify(this.regTest) },
    ]);
  }

  onAddStudentsClick() {
    const initialState = {
      regTest: this.regTest,
      updateTests: this.updateTests
    };
    this.addStudentsModal = this.modalService.show(AddStudentsModalComponent, {
      initialState,
    });
    
  }

  onScoreClicks() {
    this.router.navigate([
      'regulations-tests/scores',
      {data: JSON.stringify(this.regTest)}
    ])
  }
}
