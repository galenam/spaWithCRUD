import { Component, OnInit, Input } from '@angular/core';
import { Department } from '../department';
import { DepartmentService } from '../department.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.less']
})
export class DepartmentComponent implements OnInit {

  departments: Department[];
  @Input() departmentid: number;

  constructor(private departmentService: DepartmentService) { }

  ngOnInit() {
    this.getDepartments();
  }

  getDepartments(): void {
    this.departmentService.getDepartments().subscribe(departments => this.departments = departments);
  }

  isSelected(department): boolean {
    /*
    console.log('почему isSelected срабатывает 8 раз при нажатии на кнопку?');
    console.log(department);
    console.log(this.departmentid);
    */
    return department.id == this.departmentid;
  }
}
