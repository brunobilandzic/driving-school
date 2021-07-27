import { Component, OnInit} from '@angular/core';
import { AccountService } from '../_services/account.service';
import { RolesService } from '../_services/roles.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  roles: string [] = [];
  constructor(public accountService: AccountService, private rolesService: RolesService) { }

  ngOnInit(): void {
    this.rolesService.roles$
      .subscribe(roles => this.roles = roles);
  }

  login()
  {    
    this.accountService.login(this.model)
      .subscribe(response => {
        this.model = {};
      })
  }

}
