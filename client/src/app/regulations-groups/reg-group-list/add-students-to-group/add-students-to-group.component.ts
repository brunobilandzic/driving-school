import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { RegulationsGroup } from 'src/app/_models/regulations-group';
import { UserModel } from 'src/app/_models/user';
import { UsernamesToId } from 'src/app/_models/usernames-to-id';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-add-students-to-group',
  templateUrl: './add-students-to-group.component.html',
  styleUrls: ['./add-students-to-group.component.css'],
})
export class AddStudentsToGroupComponent implements OnInit {
  regulationsGroup: RegulationsGroup;
  studentsToAdd: UserModel[] = [];
  checkedStudents: string[] = [];
  allSelected: boolean = false;

  constructor(
    public bsModalRef: BsModalRef,
    private toastr: ToastrService,
    private membersService: MembersService
  ) {}

  ngOnInit(): void {
    this.membersService
      .getAllStudents(this.regulationsGroup.regulationsGroupId)
      .subscribe((students: UserModel[]) => (this.studentsToAdd = students));
  }

  onCheckboxClick(e: any) {
    let username = e.target.value;
    if (this.checkedStudents.includes(username))
      this.checkedStudents = this.checkedStudents.filter((s) => s != username);
    else this.checkedStudents.push(username);
  }

  onSelectAllClick(e: any) {
    this.checkedStudents = this.allSelected ? [] : this.studentsToAdd.map(st => st.username);
    this.allSelected = !this.allSelected;
  }

  addStudents() {
    let studentsToGroup =  new UsernamesToId();
    studentsToGroup.usernames = this.checkedStudents;
    studentsToGroup.id = this.regulationsGroup.regulationsGroupId;
    this.membersService.addToGroup(studentsToGroup)
      .subscribe(() => {
        this.toastr.success("Successfully added students to the group.")
        this.bsModalRef.hide();
      })
  }


}
