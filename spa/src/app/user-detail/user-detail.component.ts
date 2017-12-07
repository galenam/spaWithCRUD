import { Component, OnInit, Input } from '@angular/core';

import { DepartmentComponent } from '../department/department.component';
import { User } from '../user';
@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {

  @Input() user: User

  constructor() { }

  ngOnInit() {
  }

}
