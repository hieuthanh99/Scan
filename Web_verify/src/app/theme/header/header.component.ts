import {
  ChangeDetectionStrategy, Component, EventEmitter, Injector, OnInit, Output, ViewEncapsulation
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentAbstract, LocalStoreManagerService } from '@shared-sm';
import { CURRENCY_APP_CODE, CURRENCY_APP_ID, LIST_APP } from '../../shared/constants';
import { SettingsService } from './../../shared/services/settings.service';
export interface cms_app {
  stt: number;
  name: string;
  url: string;
  is_show_menu?: boolean;
  type?: string;
  sub_domain?: string;
  image?: string;
  element_name?: string;
  description?: string;
}
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent extends ComponentAbstract {
  protected componentInit(): void {
    throw new Error('Method not implemented.');
  }
  @Output() toggleSidenav = new EventEmitter<void>();
  @Output() toggleCollapsed = new EventEmitter<void>();
  lstAllApp = [];
  appSelected = '';
  appSelectedId = '';
  options;
  constructor(
    protected injector: Injector,
  ) {
    super(injector);
  }
  ngOnInit() {
    this.appSelected = localStorage.getItem(CURRENCY_APP_CODE);
    this.appSelectedId = localStorage.getItem(CURRENCY_APP_ID);
    this.lstAllApp = this.localStore.localStorageGetItem(LIST_APP);
    console.log(' this.lstAllApp', this.lstAllApp);
  }

  appCodeChange() {
    localStorage.setItem(CURRENCY_APP_CODE, this.appSelected);
    this.homePageService.setAppCode(this.appSelected);
  }
}
