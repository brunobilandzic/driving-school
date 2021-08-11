import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { RegulationsGroup } from 'src/app/_models/regulations-group';
import { MembersService } from 'src/app/_services/members.service';
import { RegulationsTestsService } from 'src/app/_services/regulations-tests.service';

@Component({
  selector: 'app-add-from-group-modal',
  templateUrl: './add-from-group-modal.component.html',
  styleUrls: ['./add-from-group-modal.component.css'],
})
export class AddFromGroupModalComponent implements OnInit {
  regulationsGroupId: number;
  regulationsGroups: RegulationsGroup[];
  @Input() regulationsTestId: number;
  @Output() onFinish = new EventEmitter<boolean>();
  constructor(
    private membersService: MembersService,
    private regTestService: RegulationsTestsService
  ) {}

  ngOnInit(): void {
    this.membersService
      .getRegulationsGroups()
      .subscribe((rgs: RegulationsGroup[]) => (this.regulationsGroups = rgs));
  }

  onSubmit() {
    this.regTestService.addGroupToRegulationsTest(this.regulationsGroupId, this.regulationsTestId)
      .subscribe(() => {
        this.onFinish.emit(true);
      })
  }
}
