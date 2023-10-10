import { Component, ElementRef, Injector, ViewChild } from '@angular/core';
import { MatInput } from '@angular/material/input';
import { MatTableDataSource } from '@angular/material/table';
import { environment } from '@env/environment';
import { ComponentAbstract, LocalStoreManagerService } from "@shared-sm";
import { finalize, takeUntil } from 'rxjs/operators';
import { CURRENCY_APP_CODE, CURRENCY_APP_ID, CURRENCY_KEYCLOAK_ID, CURRENCY_CLIENT_ID, LIST_APP, Status, USER_NAME } from '../../../shared/constants';
import { PATH } from '../constants';
import { HomePageService } from '../services/home-page.service';
import { KeycloakService } from 'keycloak-angular';
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
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss']
})

export class HomePageComponent extends ComponentAbstract {
  lstAllApp = [];
  title = '';
  searchText;
  typeForm = 'horizontal';
  displayedColumns: string[] = [
    'stt',
    'name',
    'actions'
  ];
  dataTable = [];
  userName;
  @ViewChild("inputSearch", { static: true }) inputSearch: ElementRef;
  constructor(
    protected injector: Injector,
    private keycloakService: KeycloakService
  ) {
    super(injector);
  }

  protected componentInit(): void {
    this.inputSearch.nativeElement.focus();
    this.search();
    this.userName = localStorage.getItem(USER_NAME);
  }

  /**
   * Tìm kiếm
   */
  search(): void {
    this.pageIndex = 0;
    this.pageSize = 5;
    this.opstions = {
      url: environment.hostApi,
      path: PATH.LIST,
      params: Object.assign(this.form.getRawValue(), {
        status: true,
        //isKeycloak : true
      })
    };
    this.loadDataTable();
  }

  valuechange(newValue) {
    this.searchText = newValue;
    if (newValue) {
      const dataSearch = this.lstAllApp.filter(x => x.appName?.toString()?.toLocaleLowerCase()?.includes(newValue.toString().toLocaleLowerCase()));
      this.setDataTable(dataSearch);
    } else {
      this.setDataTable(this.lstAllApp);

    }
  }

  createApp() {
    this.router.navigate(['/cms_prortal/category-public']);
  }

  // Set lại data của client_id vào localstorage và trong service
  setClientApp(row) {
    localStorage.setItem(CURRENCY_APP_CODE, row.appCode);
    localStorage.setItem(CURRENCY_APP_ID, row.id);
    localStorage.setItem(CURRENCY_KEYCLOAK_ID, row.keycloakId);
    localStorage.setItem(CURRENCY_CLIENT_ID, row.clientId);
    this.homePageService.setAppCode(row.appCode);
    this.router.navigate(['/cms_prortal/category-public']);
  }

  loadDataTable() {
    this.dataSource = new MatTableDataSource();
    this.indicator.showActivityIndicator();
    this.httpClient.get(this.opstions).pipe(
      takeUntil(this.ngUnsubscribe),
      finalize(() => this.indicator.hideActivityIndicator())
    ).subscribe((res: any) => {
      console.log('res', res);
      if (res && res.base && res.base.code === Status.SUCCESS && res.content) {
        this.hasDataSource = true;
        const page = this.pageIndex * this.pageSize;
        const data = res.content.content.filter(x => x.isKeycloak).sort((a, b) => { return <any>new Date(b.createdDate) - <any>new Date(a.createdDate); }).map((obj, index) => {
          obj.stt = page + index + 1;
          return obj;
        });
        this.lstAllApp = res.content.content.filter(x => x.isKeycloak);
        this.pageSize = 5;
        let listApp = [...res.content.content.filter(x => x.isKeycloak).map(v =>
          ({ appId: v.id, appCode: v.appCode, appName: v.appName, keycloakId: v.keycloakId, clientId: v.clientId }))];
        this.dataSource = new MatTableDataSource(data);
        this.totalItem =  this.lstAllApp.length;
        // set vào localStorage
        this.localStore.localStorageSetItem(LIST_APP, listApp)
      } else {
        this.hasDataSource = false;
      }
      this.dataSource.sort = this.sort;
      if (!this.dataSource.paginator && this.dformPagination) {
        this.dataSource.paginator = this.dformPagination.paginator;
      }
    });
  }

  setDataTable(dataSearch) {
    const page = this.pageIndex * this.pageSize;
    const data = dataSearch.sort((a, b) => { return <any>new Date(b.createdDate) - <any>new Date(a.createdDate); }).map((obj, index) => {
      obj.stt = page + index + 1;
      return obj;
    });
    this.totalItem = data.length;
    this.pageSize = 5;
    this.dataSource = new MatTableDataSource(data);
    if (!this.dataSource.paginator && this.dformPagination) {
      this.dataSource.paginator = this.dformPagination.paginator;
    }
  }

  /**
   * Xử lý đăng xuất
   */
  logout() {
    localStorage.clear();
    this.keycloakService.logout();
  }
}
