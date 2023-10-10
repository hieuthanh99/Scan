import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import * as moment from 'moment';
import { FormGroupAbstractComponent } from '../form-group.abstract.component';

@Component({
  selector: 'sm-date-control',
  templateUrl: './date-control.component.html',
  styleUrls: ['./date-control.component.scss']
})
export class DateControlComponent extends FormGroupAbstractComponent implements OnInit, AfterViewInit {
  minDate: moment.Moment;
  maxDate: moment.Moment;
  @ViewChild('inputElement') inputElement: any;

  constructor() {
    super();
  }

  ngOnInit() {
    this.minDate = moment(this.item.minDate);
    this.maxDate = moment(this.item.maxDate);
  }

  ngAfterViewInit() {
    if (this.item.focus) {
      setTimeout(() => {
        this.inputElement.nativeElement.focus();
      }, 100);
    }
  }
}

