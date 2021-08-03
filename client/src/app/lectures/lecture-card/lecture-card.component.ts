import { Component, Input, OnInit } from '@angular/core';
import { Lecture } from 'src/app/_models/lecture';

@Component({
  selector: 'app-lecture-card',
  templateUrl: './lecture-card.component.html',
  styleUrls: ['./lecture-card.component.css']
})
export class LectureCardComponent implements OnInit {
  @Input() lecture: Lecture;
  constructor() { }

  ngOnInit(): void {
  }

}
