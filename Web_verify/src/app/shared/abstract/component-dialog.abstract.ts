import { Directive, Injector, ViewChild } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Params } from '@angular/router';
import { Observable, of } from 'rxjs';
import { DformPaginationPlusComponent } from '../dform-pagination-plus/dform-pagination-plus.component';
import { LocalStoreEnum } from '../enum/local-store.enum';
import { HttpOptions } from '../models/request.base.dto';
import { APPROVER, MAKER, STATUS } from './../constants';
import { ComponentBaseAbstract } from './component.base.abstract';

@Directive()
export abstract class ComponentDialogAbstract extends ComponentBaseAbstract {

  public currenUser;
  public queryParams: Params;
  public isMaker: boolean;
  public isChecker: boolean;
  public opstions: HttpOptions;

  // Khai báo cho role
  public roles;
  public typeInput = MAKER;
  public typeApprover = APPROVER;

  // Khai báo cho table
  pageEvent: PageEvent = new PageEvent();
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('pagePage', { static: true }) dformPagination: DformPaginationPlusComponent;
  public dataSource: MatTableDataSource<any>;
  hasDataSource = false;
  public pageSize = 10;
  public pageIndex = 0;
  public totalItem = 0;

  constructor(protected injector: Injector) {
    super(injector);
  }

  protected initRole(typeInput: string, typeApprover: string) {
    this.typeInput = typeInput;
    this.typeApprover = typeApprover;
  }

  private getRoleUser() {
    // Lấy thông tin quyền user
    this.currenUser = this.localStore.getData(LocalStoreEnum.User_Infor);
    if (this.currenUser) {
      const listMaker = this.currenUser.authorities.filter(x => x.toUpperCase() === this.typeInput);
      const listCHECKER = this.currenUser.authorities.filter(x => x.toUpperCase() === this.typeApprover);
      this.isMaker = listMaker.length > 0;
      this.isChecker = listCHECKER.length > 0;
      this.roles = this.currenUser.authorities || [];
      if (this.currenUser && this.currenUser.user_name && this.currenUser.user_name.toLowerCase() === 'admin') {
        this.isMaker = true;
        this.isChecker = true;
      }
    }
  }

  goTo(state: string, params?: any, replaceUrl?: boolean, relativeTo?: ActivatedRoute) {
    replaceUrl = replaceUrl == null ? false : replaceUrl;
    this.router.navigate([state], { queryParams: params, relativeTo, replaceUrl });
  }

  goTo404() {
    this.router.navigate(['error'], { replaceUrl: true });
  }

  public getLabel($status) {
    const status = STATUS.find(item => item.key === $status);
    if (status) {
      return `<label class="wf-status ${status.class}">${status.value}</label>`;
    } else {
      return '';
    }
  }

  back() {
    window.history.back();
  }

  protected cloneData(data) {
    return JSON.parse(JSON.stringify(data));
  }

  /**
   * @author PHUONGPV
   * Auto scroll to control validate false
   */
  scrollIntoViewErrorValidate() {
    const element = document.querySelector('.error');
    if (element) {
      element.scrollIntoView({ behavior: 'smooth', block: 'center' });
    }
  }

  listButtonDynamic(status: string, ...list: any[]): Observable<any[]> {
    return of(list.filter(res => !res.status || (res.status && res.status.includes(status))));
  }

}
