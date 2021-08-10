import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import SharedModule from './_modules/shared-module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ErrorsInterceptor } from './_interceptors/errors.interceptor';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { MembersComponent } from './members/members.component';
import { RegisterComponent } from './register/register.component';
import { AddLectureTopicComponent } from './lectures/add-lecture-topic/add-lecture-topic.component';
import { HoldLectureComponent } from './lectures/hold-lecture/hold-lecture.component';
import { TextInputComponent } from './_inputs/text-input/text-input.component';
import { DateInputComponent } from './_inputs/date-input/date-input.component';
import { LecturesHomeProfessorComponent } from './lectures/lectures-home-professor/lectures-home-professor.component';
import { TimeInputComponent } from './_inputs/time-input/time-input.component';
import { LecturesListComponent } from './lectures/lectures-list/lectures-list.component';
import { LectureCardComponent } from './lectures/lecture-card/lecture-card.component';
import { LectureViewComponent } from './lectures/lecture-view/lecture-view.component';
import { LoadingInterceptor } from './_interceptors/loading.interceptor';
import { LectureAttendanceComponent } from './lectures/lecture-attendance/lecture-attendance.component';
import { ViewStudentComponent } from './view-student/view-student.component';
import { ProfessorStudentComponent } from './view-student/professor-student/professor-student.component';
import { InstructorStudentComponent } from './view-student/instructor-student/instructor-student.component';
import { StudentListComponent } from './view-student/student-list/student-list.component';
import { StudentInfoComponent } from './view-student/student-info/student-info.component';
import { AddToGroupComponent } from './view-student/student-info/add-to-group/add-to-group.component';
import { SignToTestComponent } from './view-student/student-info/sign-to-test/sign-to-test.component';
import { StudentLectureListComponent } from './view-student/student-lecture-list/student-lecture-list.component';
import { StudentLectureCardComponent } from './view-student/student-lecture-list/student-lecture-card/student-lecture-card.component';
import { TestsListComponent } from './regulations-tests/tests-list/tests-list.component';
import { TestCardComponent } from './regulations-tests/test-card/test-card.component';
import { TestsPanelComponent } from './regulations-tests/tests-panel/tests-panel.component';
import { CreateRegTestComponent } from './regulations-tests/create-reg-test/create-reg-test.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MembersComponent,
    RegisterComponent,
    AddLectureTopicComponent,
    HoldLectureComponent,
    TextInputComponent,
    DateInputComponent,
    LecturesHomeProfessorComponent,
    TimeInputComponent,
    LecturesListComponent,
    LectureCardComponent,
    LectureViewComponent,
    LectureAttendanceComponent,
    ViewStudentComponent,
    ProfessorStudentComponent,
    InstructorStudentComponent,
    StudentListComponent,
    StudentInfoComponent,
    AddToGroupComponent,
    SignToTestComponent,
    StudentLectureListComponent,
    StudentLectureCardComponent,
    TestsListComponent,
    TestCardComponent,
    TestsPanelComponent,
    CreateRegTestComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    SharedModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorsInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],

})
export class AppModule { }
