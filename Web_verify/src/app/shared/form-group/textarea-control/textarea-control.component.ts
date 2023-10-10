import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { FormGroupAbstractComponent } from '../form-group.abstract.component';

@Component({
  selector: 'sm-textarea-control',
  templateUrl: './textarea-control.component.html',
  styleUrls: ['./textarea-control.component.scss']
})
export class TextareaControlComponent extends FormGroupAbstractComponent implements AfterViewInit {

  @ViewChild('inputElement') inputElement: any;
  thecontents: any;

  constructor() {
    super();
  }

  ngAfterViewInit() {
    if (this.item.focus) {
      setTimeout(() => {
        this.inputElement.nativeElement.focus();
      }, 100);
    }
  }

  onChangeData(event: any) {
  }
}
