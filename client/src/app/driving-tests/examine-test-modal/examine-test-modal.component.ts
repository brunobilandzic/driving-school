import { Component, EventEmitter, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { DrivingTestModel } from 'src/app/_models/driving-test';
import { DrivingService } from 'src/app/_services/driving.service';

@Component({
  selector: 'app-examine-test-modal',
  templateUrl: './examine-test-modal.component.html',
  styleUrls: ['./examine-test-modal.component.css']
})
export class ExamineTestModalComponent implements OnInit {
  drivingTest: DrivingTestModel;
  examinerRemark: string;
  isPassed: boolean;
  invokeRefresh: EventEmitter<boolean>;
  constructor(public bsModalRef: BsModalRef, private drivingService: DrivingService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.examinerRemark = this.drivingTest.examinerRemarks;
    this.isPassed = this.drivingTest.passed;
    }

  onSubmit(){
    let examination = {};
    examination["drivingTestId"]= this.drivingTest.drivingSessionId;
    examination["passed"]=this.isPassed;
    examination["examinerRemarks"]=this.examinerRemark;

    this.drivingService.examineTest(examination)
      .subscribe(() => 
        {
          this.toastr.success("Successfully updated driving test.");
          this.invokeRefresh.emit(true);
          this.bsModalRef.hide();
        }
      )
  }

}
