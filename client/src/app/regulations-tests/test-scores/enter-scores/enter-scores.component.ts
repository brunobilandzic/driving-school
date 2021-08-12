import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { RegulationsTestModel } from 'src/app/_models/regulations-test';
import { StudentScore } from 'src/app/_models/student-score';
import { RegulationsTestsService } from 'src/app/_services/regulations-tests.service';

@Component({
  selector: 'app-enter-scores',
  templateUrl: './enter-scores.component.html',
  styleUrls: ['./enter-scores.component.css'],
})
export class EnterScoresComponent implements OnInit {
  @Input() regTest: RegulationsTestModel;
  enterScores = {};
  constructor(private regTestService: RegulationsTestsService,
      private toastr: ToastrService
    ) {}

  ngOnInit(): void {
    this.regTest.studentRegulationsTest.forEach((studentScore) => {
      console.log(studentScore);
      this.enterScores[studentScore.studentUsername] = studentScore.score;
    });

    console.log(this.enterScores);
  }

  onScoreChange(e: any) {
    console.log(e.target.value);
  }

  onSubmitScores() {
    console.log(this.enterScores);
    let studentScores: StudentScore[] = [];

    Object.keys(this.enterScores).forEach((k) => {
      //k is username
      let newStudentScore: StudentScore = new StudentScore;
      newStudentScore.score = this.enterScores[k];
      newStudentScore.username = k;
      studentScores.push(newStudentScore);
    });

    this.regTestService.examineRegulationsTest(this.regTest.regulationsTestId, studentScores)
      .subscribe(() => {
        this.toastr.success("Successfully examined students.");
        this.updateClientScores(studentScores);
        
      })
  }

  updateClientScores(studentsScores: StudentScore[])
  {
    studentsScores.forEach(studentScore => {
      this.regTest.studentRegulationsTest.forEach((elem, i) => {
        if (elem.studentUsername == studentScore.username) elem.score = studentScore.score
      })
    });
  }
}
