import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { fromEvent } from 'rxjs';
import { convertToObject } from 'typescript';
import { RolesService } from '../_services/roles.service';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css'],
})
export class ViewStudentComponent implements OnInit {
  professorView = false;
  instructorView = false;
  forceView = '';
  loading = true;
  username: string;
  constructor(
    public rolesService: RolesService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.rolesService.roles$.subscribe((roles) => {
      if (roles.length == 2) {
        // User is not both instructor and professor
        if (roles.includes('Professor')) this.professorView = true;
        else if (roles.includes('Instructor')) this.instructorView = true;
      }
      this.loading = false;
      return;
    });

    this.route.queryParamMap.subscribe((queryParamMap) => {
      switch (queryParamMap.get("mode")) {
        case 'instructor':
          this.forceView = 'instructor';
          break;
        case 'professor':
          this.forceView = 'professor';
          break;
        default:
          break;
      }
      
    });

    this.route.data.subscribe(data => this.username = data.id);

    
  }

  toggleToInstructor(e: any)
  {
    this.forceView = '';
    this.professorView = false;
    this.instructorView = true;
  }

  toggleToProfessor(e: any)
  {
    this.forceView = '';
    this.instructorView = false;
    this.professorView = true;
  }

}
