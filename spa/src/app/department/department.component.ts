import { Component, OnInit } from '@angular/core';
import { Department } from '../department';
import { DEPARTMENTS } from '../mock-departments';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  departments = DEPARTMENTS;

  constructor() { }

  ngOnInit() {
  }

}
