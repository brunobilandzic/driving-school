import { Component, OnInit } from '@angular/core';
import { RolesService } from 'src/app/_services/roles.service';

@Component({
  selector: 'app-session-panel',
  templateUrl: './session-panel.component.html',
  styleUrls: ['./session-panel.component.css'],
})
export class SessionPanelComponent implements OnInit {
  roles: string[] = [];
  constructor(private rolesService: RolesService) {}

  ngOnInit(): void {
    this.rolesService.roles$.subscribe(
      (roles: string[]) => (this.roles = roles)
    );
  }
}
