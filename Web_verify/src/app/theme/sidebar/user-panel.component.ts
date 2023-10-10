import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-panel',
  template: `
    <a fxLayout="column" fxLayoutAlign="start start"  [routerLink]="'/dashboard'">
      <img
        class="{{class}}"
        src="{{imageUrl}}"
        alt="avatar"
      />
      <img
        class="dform-item-panel-avatar"
        src="assets/images/favicon.ico"
        alt="avatar"
      />
</a>
  `,
  styleUrls: ['./user-panel.component.scss'],
})
export class UserPanelComponent {
  imageUrl: string = 'assets/images/logo.svg';
  class: string = 'dform-user-panel-avatar';
  constructor(private router: Router) {
    if (this.router.url.includes('emb')) {
      this.imageUrl = 'assets/images/biz.svg';
      this.class = 'dform-user-panel-avatar-biz';
    };
  }
}
