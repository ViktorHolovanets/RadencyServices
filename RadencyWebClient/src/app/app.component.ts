import { Component } from '@angular/core';
import {ModalService} from "./services/modal.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title="Library Radency"
  constructor(public modalService:ModalService) {
  }
}
