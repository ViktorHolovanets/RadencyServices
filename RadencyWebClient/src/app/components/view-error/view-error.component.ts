import { Component } from '@angular/core';
import {ErrorService} from "../../services/error.service";

@Component({
  selector: 'app-view-error',
  templateUrl: './view-error.component.html',
  styleUrls: ['./view-error.component.scss']
})
export class ViewErrorComponent {
constructor(public  errorService: ErrorService) {
}
}
