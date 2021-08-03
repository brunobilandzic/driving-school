import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LectureTopic } from 'src/app/_models/lecture-topic';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-lecture-topic',
  templateUrl: './add-lecture-topic.component.html',
  styleUrls: ['./add-lecture-topic.component.css']
})
export class AddLectureTopicComponent implements OnInit {
  form: FormGroup;
  baseUrl = environment.baseApiUrl + 'professor/lecture-topics'

  constructor(private fb: FormBuilder, private http: HttpClient, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required]
    })
  }

  submit()
  {
    console.log(this.form.value);

    this.http.post(this.baseUrl, this.form.value)
      .subscribe((lt: LectureTopic) => {
        this.form.reset();
        this.toastr.success(`'${lt.title}' created!`)
      });
  }

}
