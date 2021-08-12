import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DrivingSessionModel } from 'src/app/_models/driving-session';
import { StudentModel } from 'src/app/_models/student';
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
    });
    this.route.paramMap.subscribe((params) => {
      if (params == null) return;
      this.drivingSessionEdit = JSON.parse(params.get('data'));
    });
  }

  ngOnInit(): void {
    this.membersService
      .getAllStudents()
      .subscribe((students: StudentModel[]) => {
        this.drivers = students.map((s) => s.username);
        console.log(this.drivers);
      });

    this.populateForm();
  }

  populateForm() {
    if (this.drivingSessionEdit == undefined) return;
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

    if (this.drivingSessionEdit == undefined) {
      this.drivingService
        .createDrivingSession(postSession)
        .subscribe((ds: DrivingSessionModel) => {
          this.toastr.success(
            'Successfully created driving session ' + ds.drivingSessionId
          );
        });
    } else {
      postSession['instructorRemarks'] = this.drivingSessionEdit.instructorRemarks;
      postSession['studentRemarks'] = this.drivingSessionEdit.driverRemarks;
      postSession['drivingSessionId'] = this.drivingSessionEdit.drivingSessionId;
      this.drivingService.updateDrivingSession(postSession)
        .subscribe(() => {
          this.toastr.success("Successfully updated driving session.")
        })

    }
  }


  isEditMode() {
    return this.drivingSessionEdit != undefined
  }
}
