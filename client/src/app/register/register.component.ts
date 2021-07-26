import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { UserModel } from '../_models/user-model';
import { AccountService } from '../_services/account.service';

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
  };
  baseUrl = environment.baseApiUrl + "account/register"
  constructor(
    private accountService: AccountService,
    private toastr: ToastrService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {}

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

    this.register()
      .subscribe((response: UserModel) => {
        this.accountService.setCurrentUser(response);
        this.toastr.success("Successfully registered!");
        this.accountService.navigateTo("/members");
      })
  }

  register() {
    return this.http.post(this.baseUrl, this.model);
  }

  reset() {
    for (let key in this.model) {
      this.model[key] = '';
    }
  }
}
