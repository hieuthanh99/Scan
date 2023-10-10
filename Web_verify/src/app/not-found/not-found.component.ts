import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'mbb-page-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.scss']
})
export class NotFoundComponent {

  public title = 'Trang quý khách đang truy cập không tồn tại. Ngân Hàng TMCP Quân Đội xin lỗi quý khách vì sự bất tiện này.';
  public button = '';

  constructor(private router: Router) {
  }

  gotoHome() {
    this.router.navigate(['/cms_prortal'])
  }
}
