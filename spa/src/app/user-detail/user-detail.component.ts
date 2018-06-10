import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

import { DepartmentComponent } from '../department/department.component';
import { User } from '../user';

import { UserService } from '../user.service';

import { OperationTypeEnum } from '../app.component';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.less']
})
export class UserDetailComponent implements OnInit {

  @Input() user: User;
  @Output() updateUserListEvent = new EventEmitter<boolean>();
  @Output() deleteUserListEvent = new EventEmitter<number>();

  buttonName: string;
  form: FormGroup;
  userAdded: boolean;
  errorType: OperationTypeEnum;
  public operationTypeEnum = OperationTypeEnum;

  constructor(private formBuilder: FormBuilder, private userService: UserService) {
    this.form = this.formBuilder.group({
      name: [null, [Validators.required, Validators.minLength(4), Validators.pattern('[a-zA-Z]{1,30}[ ]?[a-zA-Z]{0,30}')]],
      formDepartment: [null, [Validators.min(0)]]
    });
  }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    this.errorType = null;
    
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
            user.id = result;
            this.updateUserListEvent.next(user);
            this.user = new User();
            this.user.id = -1;
            this.user.departmentId = -1;
            this.user.name = '';
            this.form.patchValue({
              name: '',
              formDepartment: -1
            });
          }
        },
          error => {
            this.errorType = OperationTypeEnum.Add;
          });
      }
      else {
        user = this.prepareUserToApi(user.id);
        this.userService.updateUser(user).subscribe(result => {
          this.updateUserListEvent.next(user);
        },
          error => {
            this.errorType = OperationTypeEnum.Update;
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

  deleteUser(): void {
    if (this.user && this.user.id > 0) {
      this.userService.deleteUser(this.user.id).subscribe(result => {
        this.deleteUserListEvent.next(this.user.id);
        this.user = null;
      },
        error => {
          this.errorType = OperationTypeEnum.Delete;
        });
    }
  }
}
