import { Component, OnInit } from '@angular/core';
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

  constructor(private userService: UserService, private departmentService: DepartmentService)
  { }

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
      // results[0] is our tmpUsers
      // results[1] is our tmpDepartments
      this.Departments = dep;
      this.Users = us.map((user1) => {
        //debugger;
        var department = this.Departments.find((element) => element.id == user1.departmentId);
        if (department != null) {
          user1.departmentName = department.name;
        }
        return user1;
      });
    });

    /*
    debugger;
    this.Users = response;
    this.departmentService.getDepartments().subscribe(
      function (response1) {
        debugger;

        this.Departments = response1;
        this.Users = this.Users.map((user1) => {
          debugger;
          var department = this.Departments.find((element) => element.id == user1.departmentid);
          if (department != null) {
            user1.departmentName = department.name;
          }
          return user1;
        });
      },
      function (error) { { console.log("Error getDepartments happened" + error) } }
    )
  },
    function (error) { { console.log("Error getUsers happened" + error) } }
  );
*/
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
    this.selectedUser.departmentId = -1;
  }
  /*
    showEditForm(): void {
      this.hideForm = !this.hideForm;
    }
  */
}
