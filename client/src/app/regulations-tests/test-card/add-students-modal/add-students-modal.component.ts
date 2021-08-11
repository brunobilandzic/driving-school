import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { RegulationsTestModel } from 'src/app/_models/regulations-test';
import { UsernamesToId } from 'src/app/_models/usernames-to-id';
import { RegulationsTestsService } from 'src/app/_services/regulations-tests.service';

@Component({
  selector: 'app-add-students-modal',
  templateUrl: './add-students-modal.component.html',
  styleUrls: ['./add-students-modal.component.css'],
})
export class AddStudentsModalComponent implements OnInit {
  regTest: RegulationsTestModel;
  updateTests: EventEmitter<boolean>;
  expanded: string = '';
  constructor(
    public bsModalRef: BsModalRef,
    private toastr: ToastrService,
    private regTestService: RegulationsTestsService
  ) {}

  ngOnInit(): void {}

  onFinish(e: boolean) {
    this.expanded = '';
    if (e) {
      this.toastr.success('Successfully updated regulations test.');
      this.regTestService
        .fetchRegulationsTest(this.regTest.regulationsTestId)
        .subscribe((rt: RegulationsTestModel) => {
          this.regTest = rt;
          this.updateTests.emit(true);
          this.bsModalRef.hide();
        });
    } else {
      this.toastr.error('Failed to update regulations test.');
    }
  }

  onDeleteAll() {
    if (this.regTest.studentRegulationsTest.length == 0)
      return this.toastr.error('Regulations test already empty.');
    if (
      window.confirm(
        `Are you sure you want to delete ${this.regTest.studentRegulationsTest.length} students from regulations test ${this.regTest.regulationsTestId}`
      ) == false
    )
      return;

    let studentsToDelete = this.regTest.studentRegulationsTest.map(
      (srt) => srt.studentUsername
    );

    this.regTestService
      .deleteStudentsFromRegulationsTest(
        studentsToDelete,
        this.regTest.regulationsTestId
      )
      .subscribe(() => {
        this.toastr.success(
          'Successfully deleted all students from regulations test.'
        );
        this.regTest.studentRegulationsTest = [];
        this.updateTests.emit(true);
        this.bsModalRef.hide();
      });
  }
}
