import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { DrivingService } from 'src/app/_services/driving.service';

@Component({
  selector: 'app-remark-input',
  templateUrl: './remark-input.component.html',
  styleUrls: ['./remark-input.component.css']
})
export class RemarkInputComponent implements OnInit {
  instructorRemarks: string = "";

  drivingSessionId: number;
  refreshSessionList: EventEmitter<string>;
  constructor(
    public bsModalRef: BsModalRef,
    private drivingService: DrivingService,
    private toastr: ToastrService
    ) { }

  ngOnInit(): void {

  }

  onSubmit() {
    let instructorRemarks = {}
    instructorRemarks["instructorRemarks"] = this.instructorRemarks
    instructorRemarks["drivingSessionId"]= this.drivingSessionId;

    this.drivingService.putInstructorRemarks(instructorRemarks)
      .subscribe(() => {
        this.toastr.success("Remark successfully put.")
        this.refreshSessionList.emit(this.instructorRemarks);
      })
  }

}
