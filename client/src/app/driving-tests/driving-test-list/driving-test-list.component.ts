import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { DrivingTestModel } from 'src/app/_models/driving-test';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { RoleModel } from 'src/app/_models/role';
import { DrivingService } from 'src/app/_services/driving.service';
import { RolesService } from 'src/app/_services/roles.service';

@Component({
  selector: 'app-driving-test-list',
  templateUrl: './driving-test-list.component.html',
  styleUrls: ['./driving-test-list.component.css'],
})
export class DrivingTestListComponent implements OnInit {
  drivingTests: DrivingTestModel[];
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination;
  role: string = "";
  constructor(private rolesService: RolesService, private toastr: ToastrService, private drivingService: DrivingService) {}

  ngOnInit(): void {
    let roleModel = new RoleModel();
    this.rolesService.roles$.subscribe((roles: string []) => {
      this.role = roles.find(r => [roleModel.Instructor, roleModel.Examiner, roleModel.Student].includes(r));

      if(this.role == undefined){
        this.toastr.error("You are not Instructor, Examiner nor Student.");
        return;
      }

      this.fetchTests();
    })
  }

  fetchTests() {
    this.drivingService.getTestsFor(this.role, this.pageNumber, this.pageSize)
      .subscribe((paginatedResult: PaginatedResult<DrivingTestModel []>) => {
        console.log(paginatedResult);
        this.drivingTests = paginatedResult.result;
        this.pagination = paginatedResult.pagination;
      })
  }

  deleteTest(drivingSessionId: number) {
    this.drivingService.drivingTests = new Map();
    this.drivingService.deleteTest(drivingSessionId)
      .subscribe(() => {
        this.toastr.success("Successfully deleted test "  + drivingSessionId);
        this.fetchTests();
      })
  }

  refreshTests(e: boolean) {
    if(!e) return;
    this.drivingService.drivingTests = new Map();
    this.fetchTests();
  }
}
