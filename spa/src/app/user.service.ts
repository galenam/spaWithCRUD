import { Injectable } from '@angular/core';
import { User } from './user';
import { _throw } from 'rxjs/observable/throw';
import { Observable, } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { RequestOptions, RequestOptionsArgs } from '@angular/http';
import { HttpClient, HttpHeaders, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
@Injectable()
export class UserService {

  private userURL = 'http://localhost:5200/api/user'

  constructor(private http: HttpClient) {

  }

  getUsers(): Observable<User[]> {
    var result = this.http.get<User[]>(this.userURL);
    return result;
  }

  addUser(user): Observable<number> {
    var result = this.http.post<number>(this.userURL, user);
    return result;
  }

  updateUser(user): Observable<object> {
    var putUrl = this.userURL + "/" + user.id;
    var result = this.http.put(putUrl, user).pipe(
      catchError(this.handleError)
    );
    return result;
  }

  deleteUser(id): Observable<object> {
    var deleteUrl = this.userURL + "/" + id;
    var result = this.http.delete(deleteUrl).pipe(
      catchError(this.handleError)
    );
    return result;
  }


  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.log('An error occurred:', error.error.message);
    } else if (error.status) {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.log(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    else {
      console.log(
        `Backend failed` + error.message);
    }
    // return an observable with a user-facing error message
    return _throw(
      'Something bad happened; please try again later.');
  };
}
