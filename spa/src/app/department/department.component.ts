import { Component, OnInit, Input, OnChanges, forwardRef } from '@angular/core';
import { ControlValueAccessor, FormGroup, FormBuilder, Validators, NG_VALUE_ACCESSOR } from '@angular/forms';

import { Department } from '../department';
import { DepartmentService } from '../department.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.less'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => DepartmentComponent),
    }]
})
export class DepartmentComponent implements OnInit, ControlValueAccessor {
  writeValue(value: any): void {
    this.formDepartment.patchValue({
      departmentControl: value
    });
  }

  registerOnChange(fn: any): void {
    this.formDepartment.valueChanges.subscribe(fn);
  }

  registerOnTouched(fn: any): void {

  }
  setDisabledState?(isDisabled: boolean): void {
  }

  departments: Department[];
  @Input() departmentid: number;
  formDepartment: FormGroup;


  constructor(private formBuilder: FormBuilder, private departmentService: DepartmentService) {
    this.formDepartment = this.formBuilder.group({
      departmentControl: [null, [Validators.required, Validators.min(0)]]
    });
  }
  ngOnInit() {
    this.getDepartments();

  }

  getDepartments(): void {
    this.departmentService.getDepartments().subscribe(departments => this.departments = departments);
  }
}
