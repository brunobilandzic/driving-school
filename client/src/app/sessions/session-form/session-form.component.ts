import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DrivingSessionModel } from 'src/app/_models/driving-session';
import { StudentModel } from 'src/app/_models/student';
import { UserModel } from 'src/app/_models/user';
import { DrivingService } from 'src/app/_services/driving.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-session-form',
  templateUrl: './session-form.component.html',
  styleUrls: ['./session-form.component.css'],
})
export class SessionFormComponent implements OnInit {
  drivingSessionEdit: any;
  drivingSession: FormGroup;
  drivers: string[];
  minDate = new Date();
  examinerEdit = false;
  testForm = false;
  instructors: string[];
  createForDriver = false;
  constructor(
    private fb: FormBuilder,
    private membersService: MembersService,
    private drivingService: DrivingService,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ) {
    this.drivingSession = this.fb.group({
      dateStart: ['', Validators.required],
      dateTime: ['', Validators.required],
      driverUsername: ['', Validators.required],
      hours: [1, [Validators.min(1), Validators.max(2)]],
      instructorUsername: [''],
    });
    this.route.paramMap.subscribe((params) => {
      if (params == null) return;
      console.log(params);
      this.drivingSessionEdit = JSON.parse(params.get('data'));
      if (params.get('examinerEdit')) this.examinerEdit = true;
      if (params.get('testForm')) this.testForm = true;
      if  (params.get('createForDriver')) this.createForDriver = true;
    });
  }

  ngOnInit(): void {
    this.membersService
      .getAllStudents()
      .subscribe((students: StudentModel[]) => {
        this.drivers = students.map((s) => s.username);
        console.log(this.drivers);
      });

    this.membersService
      .getUsersFromRole('Instructor')
      .subscribe((users: UserModel[]) => {
        console.log(users);
        this.instructors = users.map((u) => u.username);
      });
    this.populateForm();
  }

  populateForm() {
    if (this.drivingSessionEdit == undefined) return;
    if(this.createForDriver) {
      this.drivingSession.controls['driverUsername'].setValue(this.drivingSessionEdit.driverUsername);
      return;
    }
    this.drivingSessionEdit.dateStart = new Date(
      this.drivingSessionEdit.dateStart
    ).toUTCString();
    Object.keys(this.drivingSessionEdit).forEach((k) => {
      this.drivingSession.controls[k]?.setValue(this.drivingSessionEdit[k]);
    });
    this.drivingSession.controls['dateTime'].setValue(
      this.drivingSessionEdit['dateStart']
    );
  }

  onSubmit() {
    let postSession = {};
    let dateStart = new Date(this.drivingSession.value.dateStart);
    let dateTime = new Date(this.drivingSession.value.dateTime);

    dateStart.setHours(dateTime.getHours());
    dateStart.setMinutes(dateTime.getMinutes());

    postSession['dateStart'] = dateStart;
    postSession['driverUsername'] = this.drivingSession.value.driverUsername;
    postSession['hours'] = this.drivingSession.value.hours;

    if (this.drivingSessionEdit == undefined || this.createForDriver) {
      if (this.testForm == false) {
        this.drivingService
          .createDrivingSession(postSession)
          .subscribe((ds: DrivingSessionModel) => {
            this.toastr.success(
              'Successfully created driving session ' + ds.drivingSessionId
            );
          });
      } else {
        postSession['instructorUsername'] =
          this.drivingSession.controls['instructorUsername'].value;
        this.drivingService.createTest(postSession).subscribe((dt: any) => {
          this.toastr.success(
            'Successfully created driving test ' + dt.drivingSessionId
          );
        });
      }
    } else {
      postSession['instructorRemarks'] =
        this.drivingSessionEdit.instructorRemarks;
      postSession['studentRemarks'] = this.drivingSessionEdit.driverRemarks;
      postSession['drivingSessionId'] =
        this.drivingSessionEdit.drivingSessionId;
      if (this.examinerEdit) {
        this.drivingService
          .updateDrivingSessionExaminer(postSession)
          .subscribe(() => {
            this.toastr.success('Successfully updated driving session.');
          });
      } else {
        this.drivingService.updateDrivingSession(postSession).subscribe(() => {
          this.toastr.success('Successfully updated driving session.');
        });
      }
    }
  }

  isEditMode() {
    return this.drivingSessionEdit != undefined;
  }
}
