import { Component, Input, OnInit } from '@angular/core';
import {
  Form,
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserModel } from 'src/app/_models/user';
import { MembersService } from 'src/app/_services/members.service';
import { RegulationsTestsService } from 'src/app/_services/regulations-tests.service';

@Component({
  selector: 'app-create-reg-test',
  templateUrl: './create-reg-test.component.html',
  styleUrls: ['./create-reg-test.component.css'],
})
export class CreateRegTestComponent implements OnInit {
  testModel: FormGroup;
  testData: any;
  studentsCollapsed = false;
  students: string[] = [];
  allStudents: string[] = [];
  allChecked = false;
  studentsAssigned: string[]= [];
  minDate: Date = new Date(Date.now());

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private membersService: MembersService,
    private regTestsService: RegulationsTestsService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      this.testData = JSON.parse(params.get('data'));
      console.log(this.testData);
    });
    this.membersService
      .getAllStudents()
      .subscribe((students: Array<UserModel>) => {
        this.allStudents = students.map((s) => s.username);
        console.log(this.allStudents);
      });
    this.testModel = this.fb.group({
      dateStart: ['', Validators.required],
      location: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.populateForm();
  }

  populateForm() {
    if (this.testData == undefined) return;
    console.log(this.testData.dateStart);
    this.testData.dateStart = new Date(this.testData.dateStart).toUTCString();
    console.log(new Date(this.testData.dateStart).toISOString());
    Object.keys(this.testData).forEach((k) => {
      this.testModel.controls[k]?.setValue(this.testData[k]);
    });
    this.studentsAssigned = this.testData.studentRegulationsTest.map(
      (s) => s.studentUsername
    );
    console.log(this.testData);

    console.log(this.testModel.value);
    console.log(this.students);
  }

  handleStudentClick(e: any) {
    let student = e.target.value;
    if (student == 'all') {
      if (this.allChecked == false) {
        this.students = [...this.allStudents.filter(s => this.studentsAssigned.includes(s) == false)];
        this.allChecked = true;
      } else {
        this.students = [];
        this.allChecked = false;
      }
    } else {
      if (this.students.includes(student))
        this.students.splice(this.students.indexOf(student), 1);
      else this.students.push(student);
    }

    // console.log(this.students);
  }

  onSubmit() {
    let submitValue = new Object();

    submitValue = { ...this.testModel.value };
    submitValue['students'] = [...this.students];
    submitValue['dateStart'] = new Date(submitValue['dateStart']).toISOString();
    console.log(submitValue);

    if (this.testData) {
      this.regTestsService
        .editRegulationsTest(submitValue, this.testData.regulationsTestId)
        .subscribe(() => {
          this.toastr.success(
            'Successfully updated regulations test'
          );
          this.resetForm();
        });
    } else {
      this.regTestsService.addRegulationsTest(submitValue).subscribe(() => {
        this.toastr.success(
          'Successfully added new regulations test'
        );
        this.resetForm();
      });
    }
  }

  resetForm(){
    this.testModel.reset();
    this.students = [];
    this.allStudents = [];
  }
}
