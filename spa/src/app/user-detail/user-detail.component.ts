import { Component, OnInit, Input } from '@angular/core';

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

  constructor() { }

  ngOnInit() {
    
  }

  getId(departmentId): number {
    if (departmentId) return departmentId;
    return -1;
  }

  addUser():void
  {}

  getName():string
  {
    if(this.user!=null && this.user.id>0) {return "Update"}
    else {return "Add"};
  }
}
