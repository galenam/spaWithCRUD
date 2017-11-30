import { Component, OnInit } from '@angular/core';

import { User } from '../user';
import { DepartmentComponent } from '../department/department.component';
import { USERS } from '../mock-user';
import { DEPARTMENTS } from '../mock-departments';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  Users = USERS.map((user1) => {
    var department = DEPARTMENTS.find((element) => element.id == user1.departmentid);
    if (department != null) {
      user1.departmentName = department.name;
    }
    return user1;
  });

  user: User =
  {
    id: 1,
    name: "Vasya",
    departmentid: 1,
    departmentName: ''
  };

  constructor() { }

  ngOnInit() {
  }

}
