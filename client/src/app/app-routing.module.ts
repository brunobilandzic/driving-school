import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddLectureTopicComponent } from './lectures/add-lecture-topic/add-lecture-topic.component';
import { HoldLectureComponent } from './lectures/hold-lecture/hold-lecture.component';
import { LectureViewComponent } from './lectures/lecture-view/lecture-view.component';
import { LecturesHomeProfessorComponent } from './lectures/lectures-home-professor/lectures-home-professor.component';
import { LecturesListComponent } from './lectures/lectures-list/lectures-list.component';
import { MembersComponent } from './members/members.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './_guards/auth.guard';
import { RoleGuard } from './_guards/role.guard';
import { LectureResolver } from './_resolvers/lecture.resolver';

const routes: Routes = [
  { path: 'members', component: MembersComponent, canActivate: [AuthGuard] },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Admin', 'Professor', 'Instructor'] },
  },
  {
    path: 'lectures',
    component: LecturesHomeProfessorComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'lectures/add-topic',
    component: AddLectureTopicComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'lectures/hold-lecture',
    component: HoldLectureComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'lectures/list',
    component: LecturesListComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'lectures/list/:lectureId',
    component: LectureViewComponent,
    canActivate: [RoleGuard],
    resolve: {lecture: LectureResolver},
    data: { activationRoles: ['Professor'] },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
