import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
} from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { RolesService } from '../_services/roles.service';

@Injectable({
  providedIn: 'root',
})

// Route Guard Who takes array of roles from app.routing component
// Fetches user roles from roles.service
// Checks if any user role is in needed activationRoles

export class RoleGuard implements CanActivate {
  constructor(
    private rolesService: RolesService,
    private toastr: ToastrService
  ) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    
    let activationRoles = route.data.activationRoles as Array<string>;
    let activation = false;

    return this.rolesService.roles$.pipe(
      map((roles) => {
        roles.forEach(role => {
          console.log(role);
          if (activationRoles.includes(role)) activation = true;
        });
        if(!activation) {
          this.toastr.error("You dont have permission to create users.")
        }
        
        return activation;
      })
    );
  }
}
