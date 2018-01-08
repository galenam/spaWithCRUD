import { Component, OnInit, Input, OnChanges } from '@angular/core';
import {ControlValueAccessor, FormGroup, FormBuilder, Validators } from '@angular/forms';

import { Department } from '../department';
import { DepartmentService } from '../department.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.less']
})
export class DepartmentComponent implements OnInit, ControlValueAccessor  {
//https://stackoverflow.com/questions/40513450/controlvalueaccessor-with-multiple-formcontrol-in-child-component
  writeValue(value: any): void {
  if(value) {
    this.formDepartment.setValue(value);
}
  }
  registerOnChange(fn: any): void {
    this.formDepartment.valueChanges.subscribe(fn);
  }
  registerOnTouched(fn: any): void {
    
  }
  setDisabledState?(isDisabled: boolean): void {
    //throw new Error("Method not implemented.");
  }

  departments: Department[];
  @Input() departmentid: number;
  formDepartment: FormGroup;

  
  constructor(private formBuilder: FormBuilder, private departmentService: DepartmentService) {
    this.formDepartment = this.formBuilder.group({
      departmentControl: [null, [Validators.required]]
    });
   }
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
