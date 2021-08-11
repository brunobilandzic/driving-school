import { Component, Input, OnInit } from '@angular/core';
import { StudentTestModel } from 'src/app/_models/student-test';
import { RegulationsTestsService } from 'src/app/_services/regulations-tests.service';

@Component({
  selector: 'app-browse-scores',
  templateUrl: './browse-scores.component.html',
  styleUrls: ['./browse-scores.component.css']
})
export class BrowseScoresComponent implements OnInit {
  @Input() regTests : StudentTestModel [];
  constructor(private regTestService: RegulationsTestsService) { }

  ngOnInit(): void {
  }

}
