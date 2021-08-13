import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RolesService } from 'src/app/_services/roles.service';

@Component({
  selector: 'app-driving-tests-panel',
  templateUrl: './driving-tests-panel.component.html',
  styleUrls: ['./driving-tests-panel.component.css'],
})
export class DrivingTestsPanelComponent implements OnInit {
  roles: string[] = [];
  constructor(private router: Router, private rolesService: RolesService) {}

  ngOnInit(): void {
    this.rolesService.roles$.subscribe(
      (roles: string[]) => (this.roles = roles)
    );
  }

  addNewTestClick(e: Event) {
    e.stopPropagation();
    this.router.navigate(['/driving-tests/add-test', { testForm: true }]);
  }
}
