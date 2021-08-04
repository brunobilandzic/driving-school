import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Lecture } from 'src/app/_models/lecture';
import { LecturesService } from 'src/app/_services/lectures.service';
import { RolesService } from 'src/app/_services/roles.service';

@Component({
  selector: 'app-lecture-view',
  templateUrl: './lecture-view.component.html',
  styleUrls: ['./lecture-view.component.css'],
})
export class LectureViewComponent implements OnInit {
  lecture: Lecture;
  constructor(
    private route: ActivatedRoute,
    public rolesService: RolesService
  ) {
  }

  ngOnInit(): void {
    this.route.data.subscribe(data => this.lecture = data.lecture);
  }
}
