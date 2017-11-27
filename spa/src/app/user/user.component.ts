import { Component, OnInit } from '@angular/core';

import { User } from '../user';
import { DepartmentComponent } from '../department/department.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  user: User =
  {
    id: 1,
    name: "Vasya",
    departmentid: 1
  };

  constructor() { }

  ngOnInit() {
  }

}
