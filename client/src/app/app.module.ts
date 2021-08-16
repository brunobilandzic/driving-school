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
import { AddStudentsModalComponent } from './regulations-tests/test-card/add-students-modal/add-students-modal.component';
import { AddFromGroupModalComponent } from './regulations-tests/test-card/add-students-modal/add-from-group-modal/add-from-group-modal.component';
import { DeleteBunchModalComponent } from './regulations-tests/test-card/add-students-modal/delete-bunch-modal/delete-bunch-modal.component';
import { TestScoresComponent } from './regulations-tests/test-scores/test-scores.component';
import { BrowseScoresComponent } from './regulations-tests/test-scores/browse-scores/browse-scores.component';
import { EnterScoresComponent } from './regulations-tests/test-scores/enter-scores/enter-scores.component';
import { RegGroupListComponent } from './regulations-groups/reg-group-list/reg-group-list.component';
import { RegGroupFormComponent } from './regulations-groups/reg-group-form/reg-group-form.component';
import { AddStudentsToGroupComponent } from './regulations-groups/reg-group-list/add-students-to-group/add-students-to-group.component';
import { SessionPanelComponent } from './sessions/session-panel/session-panel.component';
import { SessionListComponent } from './sessions/session-list/session-list.component';
import { SessionCardComponent } from './sessions/session-card/session-card.component';
import { SessionFormComponent } from './sessions/session-form/session-form.component';
import { RemarkInputComponent } from './sessions/session-card/remark-input/remark-input.component';
import { DrivingTestListComponent } from './driving-tests/driving-test-list/driving-test-list.component';
import { DrivingTestsPanelComponent } from './driving-tests/driving-tests-panel/driving-tests-panel.component';
import { DrivingTestCardComponent } from './driving-tests/driving-test-card/driving-test-card.component';
import { ExamineTestModalComponent } from './driving-tests/examine-test-modal/examine-test-modal.component';
import { LectureTopicsComponent } from './lectures/lecture-topics/lecture-topics.component';
import { LecturesHomeStudentComponent } from './lectures/lectures-home-student/lectures-home-student.component';
import { StudentLecturesListComponent } from './lectures/student-lectures-list/student-lectures-list.component';
import { StudentSessionsListComponent } from './view-student/instructor-student/student-sessions-list/student-sessions-list.component';
import { InstructorStudentInfoComponent } from './view-student/instructor-student/instructor-student-info/instructor-student-info.component';


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
    CreateRegTestComponent,
    AddStudentsModalComponent,
    AddFromGroupModalComponent,
    DeleteBunchModalComponent,
    TestScoresComponent,
    BrowseScoresComponent,
    EnterScoresComponent,
    RegGroupListComponent,
    RegGroupFormComponent,
    AddStudentsToGroupComponent,
    SessionPanelComponent,
    SessionListComponent,
    SessionCardComponent,
    SessionFormComponent,
    RemarkInputComponent,
    DrivingTestListComponent,
    DrivingTestsPanelComponent,
    DrivingTestCardComponent,
    ExamineTestModalComponent,
    LectureTopicsComponent,
    LecturesHomeStudentComponent,
    StudentLecturesListComponent,
    StudentSessionsListComponent,
    InstructorStudentInfoComponent
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
