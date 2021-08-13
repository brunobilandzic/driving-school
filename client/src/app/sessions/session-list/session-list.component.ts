import { Component, OnInit } from '@angular/core';
import { DrivingSessionModel } from 'src/app/_models/driving-session';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { RoleModel } from 'src/app/_models/role';
import { DrivingService } from 'src/app/_services/driving.service';
import { RolesService } from 'src/app/_services/roles.service';

@Component({
  selector: 'app-session-list',
  templateUrl: './session-list.component.html',
  styleUrls: ['./session-list.component.css'],
})
export class SessionListComponent implements OnInit {
  drivingSessions: DrivingSessionModel[] = [];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 15;
  role: string = '';
  roles = new RoleModel();
  constructor(
    private drivingService: DrivingService,
    private rolesService: RolesService
  ) {}

  ngOnInit(): void {
    this.rolesService.roles$.subscribe((roles: string[]) => {
      this.role = roles.find((r) =>
        [this.roles.Instructor, this.roles.Student].includes(r)
      );
    });
    this.loadDrivingSessions();
  }

  loadDrivingSessions() {
    this.drivingService
      .getDrivingSessions(this.role, this.pageNumber, this.pageSize)
      .subscribe((pr: PaginatedResult<DrivingSessionModel[]>) => {
        this.drivingSessions = pr.result;
        this.pagination = pr.pagination;
      });
  }

  pageChanged(e: any) {
    this.pageNumber = e.page;
    this.loadDrivingSessions();
  }

  refreshSessionList(e: any) {
    if (!e) return;
    this.drivingService.drivingSessions = new Map();
    this.loadDrivingSessions();
  }

  onDeleteDrivingSession(drivingSessionId: number) {
    this.drivingService.deleteSession(drivingSessionId).subscribe(() => {
      this.refreshSessionList(true);
    });
  }
}
