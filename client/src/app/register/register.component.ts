import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { UserModel } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { MembersService } from '../_services/members.service';
import { RolesService } from '../_services/roles.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  model = {
    firstName: '',
    lastName: '',
    username: '',
    password: '',
    repeatPassword: '',
    regulationsGroupId: undefined
  };
  regulationsGroups: any;
  
  baseUrl = environment.baseApiUrl + 'account/register-student';
  roles: string[] = [];
  registrationSuccess = false;
  registeredUser: UserModel;
  constructor(
    private membersService: MembersService,
    private toastr: ToastrService,
    private http: HttpClient,
    private rolesService: RolesService
  ) {}

  ngOnInit(): void {
    this.rolesService.roles$.subscribe((roles) => (this.roles = roles));
    this.getRegulationsGroups();
  }

  check() {
    for (let key in this.model) {
      if (this.model[key].length == 0) {
        this.toastr.error('Please provide all fields.');
        return;
      }
    }

    if (this.model.username.length < 4 || this.model.username.length > 12) {
      this.toastr.error('Username too long or too short.');
      return;
    }
    if (this.model.password.length < 6 || this.model.password.length > 20) {
      this.toastr.error('Password too long or too short.');
      return;
    }
    if (this.model.password != this.model.repeatPassword) {
      this.toastr.error('Passwords do not match');
    }
    
    this.register().subscribe((response: UserModel) => {
      this.toastr.success(`You've Successfully registered ${response.firstName} ${response.lastName}`);
      this.registrationSuccess = true;
      this.registeredUser = response;
      
    });
  }

  getRegulationsGroups()
  {
    this.membersService.getRegulationsGroups()
      .subscribe((groups:Array<any>) => this.regulationsGroups = groups.filter((g: any) => (new Date(g.dateEnd).getTime()  > Date.now())));
  }

  register() {
    this.model["role"] = ["this.checkedRoles;"]

    return this.http.post(this.baseUrl, this.model);
    
  }

  reset() {
    for (let key in this.model) {
      this.model[key] = '';
    }
  }
}
