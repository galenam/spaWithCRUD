import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

import { AppComponent } from './app.component';
import { UserComponent } from './user/user.component';
import { DepartmentComponent } from './department/department.component';
import { UserDetailComponent } from './user-detail/user-detail.component';

import { UserService } from './user.service';
import { DepartmentService } from './department.service';

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    DepartmentComponent,
    UserDetailComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule 
  ],
  providers: [UserService, DepartmentService],
  bootstrap: [AppComponent]
})
export class AppModule { }
