import { Component, OnInit } from '@angular/core';
import { PaginatedResult, Pagination } from 'src/app/_models/pagination';
import { UserModel } from 'src/app/_models/user';
import { MembersService } from 'src/app/_services/members.service';
import { RolesService } from 'src/app/_services/roles.service';
import { URL } from 'url';

@Component({
  selector: 'app-student-list',
  templateUrl: './student-list.component.html',
  styleUrls: ['./student-list.component.css'],
})
export class StudentListComponent implements OnInit {
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination;
  students: UserModel[];
  roles: string[] = [];
  active = {
    all: false,
    instructor: false,
    professor: false,
  };
  constructor(
    private membersService: MembersService,
    private rolesService: RolesService
  ) {}

  ngOnInit(): void {
    this.rolesService.roles$.subscribe((roles) => (this.roles = roles));
  }

  loadAll() {
    Object.keys(this.active).forEach((key) => {
      if (key != 'all') this.active[key] = false;
    });
    this.active.all = true;

    this.membersService
      .getStudents('users', this.pageNumber, this.pageSize)
      .subscribe((result: PaginatedResult<UserModel []>) => {
        this.pagination = result.pagination;
        this.students = result.result;
      });
    console.log(this.active);
  }

  loadInstructors() {
    Object.keys(this.active).forEach((key) => {
      if (key != 'instructor') this.active[key] = false;
    });
    this.active.instructor = true;

    this.membersService
      .getStudents('instructor', this.pageNumber, this.pageSize)
      .subscribe((result: PaginatedResult<UserModel []>) => {
        this.pagination = result.pagination;
        this.students = result.result;
      });
  }

  loadProfessors() {
    Object.keys(this.active).forEach((key) => {
      if (key != 'professor') this.active[key] = false;
    });
    this.active.professor = true;

    this.membersService
      .getStudents('professor', this.pageNumber, this.pageSize)
      .subscribe((result: PaginatedResult<UserModel []>) => {
        this.pagination = result.pagination;
        this.students = result.result;
      });
  }
  resolveLink(username){
    let link = "/student/" + username;
    Object.keys(this.active).forEach((key) => {
      if(this.active[key]) {
        switch (key) {
          case 'instructor':
            link += '?mode=instructor'
            break;
          case 'professor':
            link += '?mode=professor'
            break;
          default:
            break;
        }
      }
    })
    console.log(link);
    return encodeURI(link);
  }

  pageChanged(e: any) {
    this.pageNumber = e.page;
    Object.keys(this.active).forEach((key) => {
      if(this.active[key]) {
        switch (key) {
          case 'all':
            this.loadAll();
            break;
          case 'instructor':
            this.loadInstructors();
            break;
          case 'professor':
            this.loadProfessors();
            break;
          default:
            break;
        }
      }
    })
  }
}
