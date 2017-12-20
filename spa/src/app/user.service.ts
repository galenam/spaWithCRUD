import { Injectable } from '@angular/core';
import { User } from './user';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class UserService {

  private userURL = 'http://localhost:5200/api/user'

  constructor(private http: HttpClient) {

  }

  getUsers(): Observable<User[]> {
    var result = this.http.get<User[]>(this.userURL);
    return result;
  }

}
