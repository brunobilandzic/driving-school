import { Component, EventEmitter, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { DrivingService } from 'src/app/_services/driving.service';

@Component({
  selector: 'app-remark-input',
  templateUrl: './remark-input.component.html',
  styleUrls: ['./remark-input.component.css']
})
export class RemarkInputComponent implements OnInit {
  remark: string = "";
  role: string = "";
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
    let remarksWrap = {}
    if(this.role == 'Instructor')
      remarksWrap["instructorRemarks"] = this.remark;
    else
      remarksWrap["driverRemarks"]= this.remark;

    remarksWrap["drivingSessionId"]= this.drivingSessionId;

    this.drivingService.putRemarks(this.role, remarksWrap)
      .subscribe(() => {
        this.toastr.success("Remark successfully put.")
        this.refreshSessionList.emit(this.remark);
      })
  }

}
