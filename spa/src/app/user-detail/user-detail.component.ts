import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';
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

  @Input() user: User
  buttonName: string;
  form: FormGroup;
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
    if (this.user != null) return this.user.departmentId;
    return -1;
  }

  addUser(user): void {
    if (this.form.valid) {
      if (user.id < 0) {
        user.name = this.form.controls['name'].value;
        debugger;
        user.departmentId = this.form.controls['formDepartment'].value.departmentControl;
        //todo : сохранить в бд 
        console.log(user);
      }
    }
  }

  getName(): string {
    if (this.user != null && this.user.id > 0) { return "Update" }
    else { return "Add" };
  }
}
