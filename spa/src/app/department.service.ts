import { Injectable } from '@angular/core';
import { Department } from './department';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class DepartmentService {

  constructor(private http: HttpClient) { }
  private departmentURL = "http://localhost:5100/api/department"
  getDepartments(): Observable<Department[]> {
    var result = this.http.get<Department[]>(this.departmentURL);
    return result;
  }
}
