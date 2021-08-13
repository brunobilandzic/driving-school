import { Component, OnInit } from '@angular/core';
import { DrivingSessionModel } from 'src/app/_models/driving-session';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { DrivingService } from 'src/app/_services/driving.service';

@Component({
  selector: 'app-session-list',
  templateUrl: './session-list.component.html',
  styleUrls: ['./session-list.component.css'],
})
export class SessionListComponent implements OnInit {
  drivingSessions: DrivingSessionModel[] = [];
  pagination: Pagination;
  pageNumber= 1;
  pageSize= 15;
  
  constructor(private drivingService: DrivingService) {}

  ngOnInit(): void {
    this.loadDrivingSessions();
  }

  loadDrivingSessions() {
    this.drivingService.getDrivingSessions(this.pageNumber, this.pageSize)
      .subscribe((pr: PaginatedResult<DrivingSessionModel []>) => {
        this.drivingSessions = pr.result;
        this.pagination = pr.pagination;
      })
  }

  pageChanged(e: any) {
    this.pageNumber = e.page;
    this.loadDrivingSessions();
  }

  refreshSessionList(e: any) {
    if(!e) return;
    this.drivingService.drivingSessions = new Map();
    this.loadDrivingSessions();
  }

  onDeleteDrivingSession(drivingSessionId: number){
    this.drivingService.deleteSession(drivingSessionId)
      .subscribe(() => {
        this.refreshSessionList(true);
      })
  }

}
