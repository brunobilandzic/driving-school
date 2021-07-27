import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MembersComponent } from './members/members.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';
import { RoleGuard } from './_guards/role.guard';
import { RolesService } from './_services/roles.service';


const routes: Routes = [
  {path: "members", component: MembersComponent, canActivate: [AuthGuard]},
  {path: "register", component: RegisterComponent, canActivate: [RoleGuard], data: {activationRoles: ["Admin", "Professor", "Instructor"]}}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
