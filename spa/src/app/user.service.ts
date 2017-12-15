import { Injectable } from '@angular/core';
import { User } from './user';
import { USERS } from './mock-user';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';

@Injectable()
export class UserService {

  constructor() { }

  getUsers(): Observable<User[]> {
    return of(USERS);
  }

}
