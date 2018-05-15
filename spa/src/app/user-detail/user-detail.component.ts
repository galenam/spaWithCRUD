import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { DepartmentComponent } from '../department/department.component';
import { User } from '../user';

import { UserService } from '../user.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.less']
})
export class UserDetailComponent implements OnInit {

  @Input() user: User;
  @Output() updateUserListEvent = new EventEmitter<boolean>();
  buttonName: string;
  form: FormGroup;
  userAdded: boolean;

  constructor(private formBuilder: FormBuilder, private userService: UserService) {
    this.form = this.formBuilder.group({
      name: [null, [Validators.required]],
      formDepartment: [null, [Validators.min(1)]]
    });
  }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    var nameValue = this.user == null ? '' : this.user.name;
    var departmentid = this.user == null ? -1 : this.user.departmentId;
    this.form.patchValue({
      name: nameValue || ''
    });
  }


  getId(): number {
    if (this.user != null) {
      return this.user.departmentId;
    }
    return -1;
  }

  addUser(user): void {
    if (this.form.valid) {
      if (user.id < 0) {
        user = this.prepareUserToApi(0);
        this.userService.addUser(user).subscribe(result => {
          this.userAdded = result > 0;
          if (this.userAdded) {
            this.updateUserListEvent.next(true);
            this.user = new User();
            this.user.id = -1;
            this.user.departmentId = -1;
            this.user.name = '';
          }
        });
      }
      else {
        user = this.prepareUserToApi(user.id);
        this.userService.updateUser(user).subscribe(result => {
          console.log(result);
        });
      }
    }
  }

  prepareUserToApi(id): User {
    var user = new User();
    user.id = id;
    user.name = this.form.controls['name'].value;
    user.departmentId = this.form.controls['formDepartment'].value.departmentControl;
    return user;
  }


  getName(): string {
    if (this.user != null && this.user.id > 0) {
      return "Update"
    }
    else { return "Add" };
  }
}
