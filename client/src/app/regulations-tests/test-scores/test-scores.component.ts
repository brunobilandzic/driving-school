import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { passedToday } from 'src/app/_helpers/dates';
import { RegulationsTestModel } from 'src/app/_models/regulations-test';

@Component({
  selector: 'app-test-scores',
  templateUrl: './test-scores.component.html',
  styleUrls: ['./test-scores.component.css']
})
export class TestScoresComponent implements OnInit {
  regTest: RegulationsTestModel;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe( params => {
      this.regTest = JSON.parse(params.get('data'));
    });
  }

  checkPassedToday() {
    return passedToday(this.regTest.dateStart.toUTCString());
  }

}
