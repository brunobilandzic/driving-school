import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class RolesService {
  roles =  new BehaviorSubject<string []>([]);
  roles$ = this.roles.asObservable();
  constructor(private accountService: AccountService) { 
    this.accountService.currentUser$.subscribe(
      user => this.roles.next(user?.roles)
    )
  }

  checkRole(roles: string[]) : Observable<Boolean> {
    return this.roles$.pipe(map(
      _roles => {
        for(let role in roles) {
          if(_roles.includes(role))
            return true;
        }
        return false;
      }))
  
    
  }
}
