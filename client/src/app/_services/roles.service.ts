import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
  roles$ =  new BehaviorSubject<string []>([]);
  constructor(private accountService: AccountService) { 
    this.accountService.currentUser$.subscribe(
      user => this.roles$.next(user?.roles)
    )
  }

  checkRole(roles: string[]) : Boolean {
    for(let role in roles) {
      if(this.roles$.value.includes(role))
        return true;
    }
    return false;
  }
}
