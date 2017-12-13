import { Component, OnInit, Input } from '@angular/core';

import { DepartmentComponent } from '../department/department.component';
import { User } from '../user';
@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.less']
})
export class UserDetailComponent implements OnInit {

  @Input() user: User

  constructor() { }

  ngOnInit() {
  }

  getId(departmentId): number {
    if (departmentId) return departmentId;
    return -1;
  }
}
