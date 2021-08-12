import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RegulationsGroup } from 'src/app/_models/regulations-group';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-reg-group-form',
  templateUrl: './reg-group-form.component.html',
  styleUrls: ['./reg-group-form.component.css'],
})
export class RegGroupFormComponent implements OnInit {
  regulationsGroup: FormGroup;
  today = new Date();
  regulationsGroupId: number = 0;
  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private membersService: MembersService,
    private toastr: ToastrService
  ) {
    this.regulationsGroup = this.fb.group({
      dateStart: ['', Validators.required],
      dateEnd: ['', Validators.required],
    });

    this.route.paramMap.subscribe((params) => {
      let parsedParams = JSON.parse(params.get('data'));
      if (parsedParams == null) return;
      this.regulationsGroupId = parsedParams.regulationsGroupId;
      parsedParams.dateStart = new Date(parsedParams.dateStart).toUTCString();
      parsedParams.dateEnd = new Date(parsedParams.dateEnd).toUTCString();

      Object.keys(parsedParams).forEach((k) => {
        this.regulationsGroup.controls[k]?.setValue(parsedParams[k]);
      });
    });
  }

  ngOnInit(): void {}

  onSubmit() {
    let regGroup  = {
      ...this.regulationsGroup.value,
    };

    console.log(regGroup);

    if (this.regulationsGroupId <= 0) {
      this.membersService.createGroup(regGroup).subscribe(() => {
        this.toastr.success('Successfully created new regulations group.');
        this.regulationsGroup.reset()
      });
    } else {
      regGroup["regulationsGroupId"] = this.regulationsGroupId;
      regGroup["dateEnd"] = new Date(regGroup["dateEnd"] ).toISOString();
      regGroup["dateStart"] = new Date(regGroup["dateStart"] ).toISOString();
      console.log(regGroup)
      this.membersService.editGroup(regGroup as RegulationsGroup)
        .subscribe(() => {
          this.toastr.success('Successfully updated regulations group.');
        })
    }
  }
}
