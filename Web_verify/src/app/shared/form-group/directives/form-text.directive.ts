import { Directive, ElementRef, HostListener, Input } from '@angular/core';
import { NgControl } from '@angular/forms';
import { turnAlphanumberic } from '../../utils/utils.function';
import { D_TEXT } from './../../constants';

/**
 * nhập liệu text
 * @description directive common for number input
 */

@Directive({
  selector: 'input[dformItem], textarea[dformItem]'
})
export class FormTextDirective {
  characterPrevention = /[^A-Za-z ]/g;
  @Input() dformItem: any;

  // eslint-disable-next-line @typescript-eslint/naming-convention, no-underscore-dangle, id-blacklist, id-match
  constructor(private _el: ElementRef,
              private control: NgControl) { }

  @HostListener('paste', ['$event']) onPaste(event: KeyboardEvent) {
  }

  @HostListener('input', ['$event']) onInputChange(event: KeyboardEvent) {
    if (this.dformItem === D_TEXT) {
      this._el.nativeElement.value = turnAlphanumberic(this._el.nativeElement.value, this.characterPrevention);
      this.control?.control?.setValue(turnAlphanumberic(this._el.nativeElement.value, this.characterPrevention));
    }
  }
}
