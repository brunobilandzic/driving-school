import { Component, OnInit } from '@angular/core';
import { Lecture } from 'src/app/_models/lecture';
import { Pagination } from 'src/app/_models/pagination';
import { LecturesService } from 'src/app/_services/lectures.service';

@Component({
  selector: 'app-lectures-list',
  templateUrl: './lectures-list.component.html',
  styleUrls: ['./lectures-list.component.css']
})
export class LecturesListComponent implements OnInit {
  pageNumber = 1;
  pageSize = 6;
  pagination: Pagination;
  lectures: Lecture[];
  constructor(private lecturesService: LecturesService) { }

  ngOnInit(): void {
    this.loadLectures();
  }

  loadLectures()
  {
    	this.lecturesService.getLectures(this.pageNumber, this.pageSize)
        .subscribe(response => {
          this.lectures = response.result;
          this.pagination = response.pagination;
        })
  }

  pageChanged(event : any)
  {
    this.pageNumber = event.page;
    this.loadLectures();
  }
}
