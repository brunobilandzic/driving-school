import { Component, OnInit } from '@angular/core';
import { LectureTopic } from 'src/app/_models/lecture-topic';
import { LecturesService } from 'src/app/_services/lectures.service';

@Component({
  selector: 'app-lecture-topics',
  templateUrl: './lecture-topics.component.html',
  styleUrls: ['./lecture-topics.component.css']
})
export class LectureTopicsComponent implements OnInit {
  lectureTopics: LectureTopic[] = [];
  constructor(private lecturesService: LecturesService) { }

  ngOnInit(): void {
    this.lecturesService.getLectureTopics()
      .subscribe((lts: LectureTopic[]) => {
        this.lectureTopics = lts
      })
  }

}
