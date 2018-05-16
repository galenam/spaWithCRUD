import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { forkJoin } from "rxjs/observable/forkJoin";

import { User } from '../user';
import { Department } from '../department';
import { modificationType } from '../modificationTypeEnum';
import { UserDetailComponent } from '../user-detail/user-detail.component';

import { UserService } from '../user.service';
import { DepartmentService } from '../department.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.less']
})
export class UserComponent implements OnInit {

  constructor(private userService: UserService, private departmentService: DepartmentService, private cdr: ChangeDetectorRef) { }

  Users: User[];
  Departments: Department[];

  selectedUser: User;
  hideForm: boolean = true;

  ngOnInit() {
    this.hideForm = true;
    this.getUsers();
  }
  //Observable 
  getUsers(): void {
    let tmpDepartments = this.departmentService.getDepartments();
    let tmpUsers = this.userService.getUsers();
    forkJoin([tmpUsers, tmpDepartments]).subscribe(([us, dep]: [User[], Department[]]) => {
      this.reloadUsers(us, dep);
    });
  }

  reloadUsers(users: User[], departments: Department[]) {
    this.Departments = departments;
    this.Users = users.map((user1) => {
      var department = this.Departments.find((element) => element.id == user1.departmentId);
      if (department != null) {
        user1.departmentName = department.name;
      }
      return user1;
    });
  }

  getUsersIfUpdated(updated: User) {
    if (updated) {
      var existed = this.Users.findIndex(us => us.id == updated.id);
      if (existed >= 0) {
        this.Users[existed].departmentId = updated.departmentId;
        this.Users[existed].name = updated.name;
      }
      else {
        this.Users.push(updated);
      }
      this.reloadUsers(this.Users, this.Departments);
    }
  }

  onSelect(user: User): void {
    this.selectedUser = user;
    this.cdr.detectChanges();
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
    this.selectedUser.id = -1;
    this.selectedUser.departmentId = -1;
    this.cdr.detectChanges();
  }
}
