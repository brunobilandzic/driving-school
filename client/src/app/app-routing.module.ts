import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DrivingTestListComponent } from './driving-tests/driving-test-list/driving-test-list.component';
import { DrivingTestsPanelComponent } from './driving-tests/driving-tests-panel/driving-tests-panel.component';
import { AddLectureTopicComponent } from './lectures/add-lecture-topic/add-lecture-topic.component';
import { HoldLectureComponent } from './lectures/hold-lecture/hold-lecture.component';
import { LectureAttendanceComponent } from './lectures/lecture-attendance/lecture-attendance.component';
import { LectureTopicsComponent } from './lectures/lecture-topics/lecture-topics.component';
import { LectureViewComponent } from './lectures/lecture-view/lecture-view.component';
import { LecturesHomeProfessorComponent } from './lectures/lectures-home-professor/lectures-home-professor.component';
import { LecturesHomeStudentComponent } from './lectures/lectures-home-student/lectures-home-student.component';
import { LecturesListComponent } from './lectures/lectures-list/lectures-list.component';
import { StudentLecturesListComponent } from './lectures/student-lectures-list/student-lectures-list.component';
import { MembersComponent } from './members/members.component';
import { RegisterComponent } from './register/register.component';
import { RegGroupFormComponent } from './regulations-groups/reg-group-form/reg-group-form.component';
import { RegGroupListComponent } from './regulations-groups/reg-group-list/reg-group-list.component';
import { CreateRegTestComponent } from './regulations-tests/create-reg-test/create-reg-test.component';
import { TestScoresComponent } from './regulations-tests/test-scores/test-scores.component';
import { TestsListComponent } from './regulations-tests/tests-list/tests-list.component';
import { TestsPanelComponent } from './regulations-tests/tests-panel/tests-panel.component';
import { SessionFormComponent } from './sessions/session-form/session-form.component';
import { SessionListComponent } from './sessions/session-list/session-list.component';
import { SessionPanelComponent } from './sessions/session-panel/session-panel.component';
import { StudentListComponent } from './view-student/student-list/student-list.component';
import { ViewStudentComponent } from './view-student/view-student.component';
import { AuthGuard } from './_guards/auth.guard';
import { RoleGuard } from './_guards/role.guard';
import { AttendanceResolver } from './_resolvers/attendance.resolver';
import { IdResolver } from './_resolvers/id.resolver';
import { LectureResolver } from './_resolvers/lecture.resolver';
import { StudentResolver } from './_resolvers/student.resolver';

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
    path: 'student/lectures',
    component: LecturesHomeStudentComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Student'] },
  },
  {
    path: 'lectures/add-topic',
    component: AddLectureTopicComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'lectures/topic-list',
    component: LectureTopicsComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor', 'Student'] },
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
    path: 'student/lectures/list',
    component: StudentLecturesListComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Student'] },
  },
  {
    path: 'lectures/list/:id',
    component: LectureViewComponent,
    canActivate: [RoleGuard],
    resolve: {lecture: LectureResolver},
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'lectures/attendances/:id',
    component: LectureAttendanceComponent,
    canActivate: [RoleGuard],
    resolve: {
      attendances: AttendanceResolver,
      id: IdResolver
    },
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'student/:id',
    component: ViewStudentComponent,
    canActivate: [RoleGuard],
    resolve: {
      id: IdResolver
    },
    data: { activationRoles: ['Professor', 'Instructor'] },
  },
  {
    path: 'students',
    component: StudentListComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor', 'Instructor'] },
  },
  {
    path: 'regulations-tests/list',
    component: TestsListComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'regulations-tests',
    component: TestsPanelComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'regulations-tests/add-test',
    component: CreateRegTestComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'regulations-tests/scores',
    component: TestScoresComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'regulations-groups',
    component: RegGroupListComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'regulations-groups/add',
    component: RegGroupFormComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Professor'] },
  },
  {
    path: 'sessions',
    component: SessionPanelComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Instructor', 'Student'] }
  },{
    path: 'sessions/list',
    component: SessionListComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Instructor', 'Student'] }
  },{
    path: 'sessions/add',
    component: SessionFormComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Instructor', 'Examiner'] }
  },{
    path: 'driving-tests',
    component: DrivingTestsPanelComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Instructor', 'Examiner', 'Student'] }
  },{
    path: 'driving-tests/list',
    component: DrivingTestListComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Instructor', 'Examiner', 'Student'] }
  },{
    path: 'driving-tests/add-test',
    component: SessionFormComponent,
    canActivate: [RoleGuard],
    data: { activationRoles: ['Examiner'] }
  }




];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
