import { Component, OnInit } from '@angular/core';

import { User } from '../user';
import { USERS } from '../mock-user';
import { DEPARTMENTS } from '../mock-departments';
import { modificationType } from '../modificationTypeEnum';
import { UserDetailComponent } from '../user-detail/user-detail.component';

import { UserService } from '../user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.less']
})
export class UserComponent implements OnInit {
  /*
    Users = USERS.map((user1) => {
      var department = DEPARTMENTS.find((element) => element.id == user1.departmentid);
      if (department != null) {
        user1.departmentName = department.name;
      }
      return user1;
    });
  */
  constructor(private userService: UserService)
  { }

  Users: User[];

  selectedUser: User;
  hideForm: boolean = true;

  ngOnInit() {
    this.hideForm = true;
    this.getUsers();
  }
  //Observable HeroService
  getUsers(): void {
    this.userService.getUsers().subscribe(users => this.Users = users);
  }

  onSelect(user: User): void {
    this.selectedUser = user;
    this.hideForm = false;
  }

  getCssClass(user: User): string {
    if (user == this.selectedUser) {
      return "selected";
    }
    return "unselected";
  }

  showAddForm(): void {
    this.hideForm = false;
    this.selectedUser = new User();
    this.selectedUser.departmentid = -1;
  }
  /*
    showEditForm(): void {
      this.hideForm = !this.hideForm;
    }
  */
}
