import { LocalStoreManagerService } from '@shared-sm';
import { Component, OnInit } from '@angular/core';
import { KeycloakService } from 'keycloak-angular';

/**
 * Xử lý hiển thị user
 * @author bondm
 */
@Component({
  selector: 'app-user',
  template: `
    <div fxLayout="row" fxLayoutAlign="space-around center">
      <button
        mat-button
        class="dform-avatar-button mrl-2"
        href="javascript:void(0)"
        [matMenuTriggerFor]="menu"
      >
        <span class="dform-username" fxHide.lt-sm>{{ userName }}</span>
        <mat-icon class="mbb-icon ic-angle_down text-custom-gray fs-16"></mat-icon>
      </button>
    </div>

    <mat-menu #menu="matMenu">
      <button mat-menu-item>
        <mat-icon class="mbb-icon ic-user"></mat-icon>
        <span>{{ userName }}</span>
      </button>
      <button mat-menu-item (click)="logout()">
        <mat-icon class="mbb-icon ic-logout"></mat-icon>
        <span>Đăng xuất</span>
      </button>
    </mat-menu>
  `,
})

export class UserComponent implements OnInit {
  userName;
  constructor(
    private localStore: LocalStoreManagerService,
    private keycloakService: KeycloakService
  ) {
  }

  ngOnInit() {
    this.userName = this.localStore.getData('USER_NAME');
  }

  /**
   * Xử lý đăng xuất
   */
  logout() {
    localStorage.clear();
    this.keycloakService.logout();
  }
}
