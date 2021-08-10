import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { passedToday } from 'src/app/_helpers/datePassedToday';
import { RegulationsTestModel } from 'src/app/_models/regulations-test';

@Component({
  selector: 'app-test-card',
  templateUrl: './test-card.component.html',
  styleUrls: ['./test-card.component.css'],
})
export class TestCardComponent implements OnInit {
  @Input() regTest: RegulationsTestModel;
  @Output() deleteTest = new EventEmitter<number>();
  constructor(private router: Router) {}

  ngOnInit(): void {}

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
}
