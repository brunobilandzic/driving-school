import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { passedToday } from 'src/app/_helpers/datePassedToday';
import { DrivingSessionModel } from 'src/app/_models/driving-session';
import { RemarkInputComponent } from './remark-input/remark-input.component';

@Component({
  selector: 'app-session-card',
  templateUrl: './session-card.component.html',
  styleUrls: ['./session-card.component.css'],
})
export class SessionCardComponent implements OnInit {
  @Input() session: DrivingSessionModel;
  @Input() role: string; 
  remarkModal: BsModalRef;
  @Output() refreshSessionList = new EventEmitter<string>();
  @Output() deleteSession = new EventEmitter<number>();
  constructor(private router: Router, private modalService: BsModalService) {}

  ngOnInit(): void {}

  onEditSession() {
    this.router.navigate([
      '/sessions/add',
      { data: JSON.stringify(this.session) },
    ]);
  }

  onRemarkClick() {
    const initialState = {
      remark: this.role=='Instructor' ?  this.session.instructorRemarks: this.session.driverRemarks,
      drivingSessionId: this.session.drivingSessionId,
      refreshSessionList: this.refreshSessionList,
      role: this.role
    };
    this.remarkModal = this.modalService.show(RemarkInputComponent, {
      initialState,
    });
  }

  sessionDriven() {
    return !passedToday(new Date(this.session.dateStart).toUTCString());
  }

  onDeleteSessionClick() {
    if (
      window.confirm(
        'Dou you want to delete session ' + this.session.drivingSessionId
      ) == false
    )
      return;

    this.deleteSession.emit(this.session.drivingSessionId);
  }
}
