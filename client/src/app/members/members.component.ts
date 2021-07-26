import { Component, OnInit } from '@angular/core';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {

  constructor(public membersService: MembersService) { }

  ngOnInit(): void {
    this.membersService.getAll();
  }

}
