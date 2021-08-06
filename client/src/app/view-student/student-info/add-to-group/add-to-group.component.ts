import { Component, OnInit, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { RegulationsGroup } from 'src/app/_models/regulations-group';
import { StudentModel } from 'src/app/_models/student';
import { UsernamesToId } from 'src/app/_models/usernames-to-id';
import { MembersService } from 'src/app/_services/members.service';


@Component({
  selector: 'app-add-to-group',
  templateUrl: './add-to-group.component.html',
  styleUrls: ['./add-to-group.component.css']
})
export class AddToGroupComponent implements OnInit {
  student: StudentModel;
  refreshStudent: EventEmitter<string>;
  regulationsGroups: RegulationsGroup [];
  regulationsGroupId: number;
  constructor(public bsModalRef: BsModalRef, public membersService: MembersService, private toastr: ToastrService) { }
  ngOnInit(): void {
    this.membersService.getRegulationsGroups()
      .subscribe((regGroups: RegulationsGroup []) => {
        this.regulationsGroups = regGroups.filter(rg => rg.regulationsGroupId != this.student.regulationsGroupId && new Date(rg.dateEnd).getTime() > Date.now());
      })
  }

  save() {
    let studentsToGroup: UsernamesToId = {
      usernames : [this.student.username],
      id: this.regulationsGroupId
    }
    
    
    this.membersService.addToGroup(studentsToGroup)
      .subscribe(() => {
        this.toastr.success(`Successfully added ${this.student.firstName} to group number ${studentsToGroup.id}`);
        this.refreshStudent.emit(this.student.username);
      })
  }

}
