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
    LectureViewComponent
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
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],

})
export class AppModule { }
