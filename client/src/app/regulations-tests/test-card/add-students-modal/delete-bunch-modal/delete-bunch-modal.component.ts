import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StudentTestModel } from 'src/app/_models/student-test';
import { RegulationsTestsService } from 'src/app/_services/regulations-tests.service';

@Component({
  selector: 'app-delete-bunch-modal',
  templateUrl: './delete-bunch-modal.component.html',
  styleUrls: ['./delete-bunch-modal.component.css']
})
export class DeleteBunchModalComponent implements OnInit {
  @Input() studentsAssigned: StudentTestModel[];
  @Input() regulationsTestId: number;
  studentsUsernames: string[] = [];
  studentsToDelete: string[] = [];
  @Output() onFinish = new EventEmitter<boolean>();
  constructor(private regTestService: RegulationsTestsService) { 
    
  }

  ngOnInit(): void {
    this.studentsUsernames = this.studentsAssigned.map(s => s.studentUsername);
  }

  handleStudentCheck(e: any) {
    let username = e.target.value;
    if(this.studentsToDelete.includes(username))
      return this.studentsToDelete.splice(this.studentsToDelete.indexOf(username), 1);
    else
      return this.studentsToDelete.push(username);
    
  }

  onSubmit() {
    this.regTestService.deleteStudentsFromRegulationsTest(this.studentsToDelete, this.regulationsTestId)
      .subscribe(() => {
        this.studentsUsernames = this.studentsUsernames.filter(s => this.studentsToDelete.includes(s) == false);
        this.onFinish.emit(true);
        this.studentsToDelete = [];
        
      })
  }



}
