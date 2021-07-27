import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MembersComponent } from './members/members.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';


const routes: Routes = [
  {path: "members", component: MembersComponent, canActivate: [AuthGuard]},
  {path: "register", component: RegisterComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
