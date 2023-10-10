import { Pipe, PipeTransform } from '@angular/core';

/**
 * @author PhongNh
 * @date 14/01/2019
 * @description thêm dấu phẩy phân chia phần nghìn cho số tiền
 */
@Pipe({
  name: 'currencyMask'
})
export class CurrencyMaskPipe implements PipeTransform {
  transform(value: number | string, isClean: boolean) {
    if (value === 0) { return 0; }
    if (!value) { return ''; }
    const language = localStorage.getItem('currentLang');
    let thousandSeperator = ',';
    if (language === 'en') {
      thousandSeperator = '.';
    }
    if (!isClean) { value = value.toString().replace(/\D/g, ''); }
    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, thousandSeperator) + 'đ';
  }
}
