import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { UserModel } from '../_models/user';
import { MembersService } from '../_services/members.service';
import { RolesService } from '../_services/roles.service';

@Component({
  selector: 'app-register-employee',
  templateUrl: './register-employee.component.html',
  styleUrls: ['./register-employee.component.css']
})
export class RegisterEmployeeComponent implements OnInit {
  model = {
    firstName: '',
    lastName: '',
    username: '',
    password: '',
    repeatPassword: '',
    roles: []
  };
  regulationsGroups: any;
  
  baseUrl = environment.baseApiUrl + 'account/register-employee';
  registrationSuccess = false;
  registeredUser: UserModel;
  constructor(
    private membersService: MembersService,
    private toastr: ToastrService,
    private http: HttpClient,
    private rolesService: RolesService
  ) {}

  ngOnInit(): void {

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

    this.model
    
    this.register().subscribe((response: UserModel) => {
      this.toastr.success(`You've Successfully registered ${response.firstName} ${response.lastName}`);
      this.registrationSuccess = true;
      this.registeredUser = response;
      
    });
  }

  resolveRoleCheck(e: any) {
    let roleChecked = e.target.value;
    if(this.model.roles.includes(roleChecked))
      this.model.roles = this.model.roles.filter(r => r != roleChecked);
    else
      this.model.roles.push(roleChecked)
    
    console.log(this.model.roles);
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
