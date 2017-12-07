import { Component, OnInit } from '@angular/core';

import { User } from '../user';
import { USERS } from '../mock-user';
import { DEPARTMENTS } from '../mock-departments';
import { modificationType } from '../modificationTypeEnum';
import { UserDetailComponent } from '../user-detail/user-detail.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.less']
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

  selectedUser: User;
  hideForm: boolean = true;

  constructor() { }

  ngOnInit() {
  }


  onSelect(user: User): void {
    this.selectedUser = user;
    this.hideForm = true;
  }

  getCssClass(user: User): string {
    if (user == this.selectedUser) {
      return "selected";
    }
    return "unselected";
  }

  showAddForm(): void {
    this.hideForm = !this.hideForm;
    this.selectedUser = new User();
  }

  showEditForm(): void {
    this.hideForm = !this.hideForm;
  }

}
