import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { DepartmentComponent } from '../department/department.component';
import { User } from '../user';
@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.less']
})
export class UserDetailComponent implements OnInit {

  @Input() user: User
  buttonName: string;
  form: FormGroup;
  constructor(private formBuilder: FormBuilder) {
    this.form = this.formBuilder.group({
      name: [null, [Validators.required]],
      formDepartment: [null, [Validators.min(1)]]
    });
   }

  ngOnInit() {
  }

  ngOnChanges()
  {
    var nameValue = this.user == null ? '' : this.user.name;
    this.form.patchValue({
      name: nameValue|| ''      
    });
  }


  getId(departmentId): number {
    if (departmentId) return departmentId;
    return -1;
  }

  addUser(user): void {
    if (this.form.valid) {
      console.log(user);
      //<p>Street value: {{ heroForm.get('address.street').value}}</p>
      console.log(this.form.value);
    }
  }

  getName(): string {
    if (this.user != null && this.user.id > 0) { return "Update" }
    else { return "Add" };
  }
}
