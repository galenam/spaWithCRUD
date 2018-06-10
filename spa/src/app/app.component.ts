import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Galina';

}

export enum OperationTypeEnum {
  Add = 0,
  Update = 1,
  Delete = 2
}
