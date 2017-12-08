import { Component, OnInit, Input } from '@angular/core';
import { Department } from '../department';
import { DEPARTMENTS } from '../mock-departments';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.less']
})
export class DepartmentComponent implements OnInit {

  departments = DEPARTMENTS;
  @Input() departmentid: number;

  constructor() { }

  ngOnInit() {
  }

  isSelected(department): boolean {
    console.log('here');
    console.log(this.departmentid == department.id);
    return this.departmentid == department.id;
  }

}
