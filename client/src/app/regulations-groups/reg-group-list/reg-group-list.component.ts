import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { RegulationsGroup } from 'src/app/_models/regulations-group';
import { MembersService } from 'src/app/_services/members.service';
import { RegGroupFormComponent } from '../reg-group-form/reg-group-form.component';
import { AddStudentsToGroupComponent } from './add-students-to-group/add-students-to-group.component';

@Component({
  selector: 'app-reg-group-list',
  templateUrl: './reg-group-list.component.html',
  styleUrls: ['./reg-group-list.component.css'],
})
export class RegGroupListComponent implements OnInit {
  regulationsGroups: RegulationsGroup[];
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination;
  addStudentsModalRef: BsModalRef;
  constructor(
    private membersService: MembersService,
    private modalService: BsModalService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadRegulationsGroups();
  }

  loadRegulationsGroups() {
    this.membersService
      .getAllRegulationsGroups(this.pageNumber, this.pageSize)
      .subscribe((result: PaginatedResult<RegulationsGroup[]>) => {
        this.regulationsGroups = result.result;
        this.pagination = result.pagination;
      });
  }

  pageChanged(e: any) {
    this.pageNumber = e.page;
    this.loadRegulationsGroups();
  }

  onAddStudentsClick(regulationsGroupId: number) {
    let initialState = {
      regulationsGroup: this.regulationsGroups.find(
        (rg) => rg.regulationsGroupId == regulationsGroupId
      ),
    };

    this.addStudentsModalRef = this.modalService.show(
      AddStudentsToGroupComponent,
      { initialState }
    );
  }

  onEditGroup(regulationsGroupId: number) {
    this.router.navigate([
      'regulations-groups/add',
      {
        data: JSON.stringify(
          this.regulationsGroups.find(
            (rg) => rg.regulationsGroupId == regulationsGroupId
          )
        ),
      },
    ]);
  }
}
