import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LectureTopic } from 'src/app/_models/lecture-topic';
import { RegulationsGroup } from 'src/app/_models/regulations-group';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-hold-lecture',
  templateUrl: './hold-lecture.component.html',
  styleUrls: ['./hold-lecture.component.css']
})
export class HoldLectureComponent implements OnInit {
  lectureTopics: LectureTopic[];
  baseUrl = environment.baseApiUrl + 'professor/'
  form: FormGroup;
  regulationsGroups: RegulationsGroup[];
  minDate = new Date();
  maxDate : Date;
  constructor(private http: HttpClient, private fb: FormBuilder, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.http.get(this.baseUrl + 'lecture-topics')
      .subscribe((lts: LectureTopic[]) => this.lectureTopics = lts);

    this.http.get(this.baseUrl + 'regulations-groups')
      .subscribe((rgs: RegulationsGroup []) => {
        this.regulationsGroups = rgs.filter(
          rg => new Date(rg.dateEnd).getTime() > Date.now())
      });
  
    this.form = this.fb.group({
      lectureTopic: ['', Validators.required],
      regulationsGroup: ['', Validators.required],
      addStudents: [false],
      professorRemark: [''],
      dateStart: ['', Validators.required],
      dateTime: ['', Validators.required]
    })    

    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() + 2);

  }
  getDateStart()
  {
    let time = new Date(this.form.value.dateTime);
    let dateStart = new Date(this.form.value.dateStart);

    dateStart.setHours(time.getHours());
    dateStart.setMinutes(time.getMinutes());

    return dateStart.toISOString();
  }
  submit()
  {
    let lecture = new Object();

    lecture["lectureTopic"] = {lectureTopicId: this.form.value.lectureTopic};
    lecture["regulationsGroupId"] = this.form.value.regulationsGroup;
    lecture["dateStart"]= this.getDateStart();
    lecture["professorRemark"] = this.form.value["professorRemark"];
    let url = this.baseUrl + 'hold-lecture?addStudents=' + (this.form.value.addStudents ? "true": "false");
    console.log(url);
    
    this.http.post(url, lecture)
      .subscribe((_lecture: any) => {
        console.log(_lecture);
        
        this.toastr.success(`Lecture created for ${new Date(_lecture.dateStart).toLocaleString()}`)
      })
  }

}
