import { Component, EventEmitter, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { RegulationsTestModel } from 'src/app/_models/regulations-test';
import { StudentModel } from 'src/app/_models/student';
import { UsernameToId } from 'src/app/_models/username-to-id';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-sign-to-test',
  templateUrl: './sign-to-test.component.html',
  styleUrls: ['./sign-to-test.component.css'],
})
export class SignToTestComponent implements OnInit {
  refreshStudent: EventEmitter<string>;
  student: StudentModel;
  regulationsTests: RegulationsTestModel[];
  regulationsTestId: number;
  constructor(
    public bsModalRef: BsModalRef,
    public membersService: MembersService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.membersService.getRegulationsTests()
      .subscribe(
        (rts: RegulationsTestModel[]) => {
          this.regulationsTests = rts.filter(
            rt => 
              new Date(rt.dateStart).getTime() > Date.now() &&
              this.student.regulationsTests.map(srt => srt.regulationsTestId).includes(rt.regulationsTestId) == false
              
              );
        });
  }

  save(){
    let studentToTest: UsernameToId = {
      username: this.student.username,
      id: this.regulationsTestId
    }

    this.membersService.signToTest(studentToTest)
      .subscribe(() => {
        this.toastr.success(`Successfully signed ${this.student.username} to regulations test no ${this.regulationsTestId}`);
        this.refreshStudent.emit(this.student.username);
      })
  }
}
