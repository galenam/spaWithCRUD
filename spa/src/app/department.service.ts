import { Injectable } from '@angular/core';
import { Department } from './department';
import { DEPARTMENTS } from './mock-departments';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

@Injectable()
export class DepartmentService {

  constructor() { }
  getDepartments(): Observable<Department[]> {
    return of(DEPARTMENTS);
  }

}
