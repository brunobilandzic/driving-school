import { Component, OnInit } from '@angular/core';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { RegulationsTestModel } from 'src/app/_models/regulations-test';
import { RegulationsTestsService } from 'src/app/_services/regulations-tests.service';

@Component({
  selector: 'app-tests-list',
  templateUrl: './tests-list.component.html',
  styleUrls: ['./tests-list.component.css'],
})
export class TestsListComponent implements OnInit {
  regulationsTests: RegulationsTestModel[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 5;
  constructor(private regTestService: RegulationsTestsService) {}

  ngOnInit(): void {
    this.loadRegulationsTests();
  }

  loadRegulationsTests() {
    this.regTestService
      .loadRegulationsTests(this.pageNumber, this.pageSize)
      .subscribe((regTests: PaginatedResult<RegulationsTestModel[]>) => {
        this.regulationsTests = regTests.result;
        this.pagination = regTests.pagination;
      });
  }

  pageChanged(e: any) {
    this.pageNumber = e.page;
    this.loadRegulationsTests();
  }

  deleteTest(testId: any) {
    this.regTestService.deleteRegulationsTest(testId)
      .subscribe(() => this.loadRegulationsTests());
  }

  updateTests(e: any){
    if(e)
    {
      console.log("updating tests..")
      this.regTestService.updateTests(this.pageNumber, this.pageSize)
        .subscribe((regTests: PaginatedResult<RegulationsTestModel[]>) => {
          console.log(regTests.result)
          this.regulationsTests = regTests.result;
          this.pagination = regTests.pagination;
        });
    }
  }
}
