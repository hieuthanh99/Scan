import {CategoryPublicService} from './../services/category-public.service';
import {UpdateCategoryComponent} from './../update-category/update-category.component';
import {
  Component,
  Injector
} from '@angular/core';
import {
  MatTableDataSource
} from '@angular/material/table';
import {
  ComponentAbstract, HttpOptions, MessageSeverity, ToastMbService
} from '@shared-sm';

import {
  CATEGORY_STATUS,
  PATH,
  ROLE_APPROVER, ROLE_MAKER
} from './../constants';
import {finalize, takeUntil} from 'rxjs';
import {environment} from '@env/environment';
import {CreateCategoryComponent} from "../create-category/create-category.component";

import ISystem from "../../category-system/models/category-system.model";
import {PageEvent} from "@angular/material/paginator";
import {commonCategoryCode, value, name, code, orderNum} from "../data-form/create-category";
import {isDefault} from "../data-form/create-category";
import {description} from "../data-form/create-category";

import {fields, search} from '../data-form/category-public';
import IUser from "../../category-user/models/category-user.model";
import {USER_STATUS} from "../../category-user/constants";

@Component({
  selector: 'app-category-public',
  templateUrl: './category-public.component.html',
  styleUrls: ['./category-public.component.scss']
})
export class CategoryPublicComponent extends ComponentAbstract {

  $search = search();
  $fields = fields();

  $code = code();
  $name = name();
  $value = value();
  $commonCategoryCode = commonCategoryCode();
  $orderNum = orderNum();
  $isDefault = isDefault();
  $description = description();

  commonCategoryCode = ''

  displayedColumns = ['stt', 'code', 'name', 'description', 'isActive', 'action'];
  dataTable: ISystem[] = []

  constructor(
    protected injector: Injector,
              private categoryPublic: CategoryPublicService,
              private _toastService: ToastMbService) {
    super(injector);
    this.initRole(ROLE_MAKER, ROLE_APPROVER);
  }


  componentInit(): void {
    this.form = this.itemControl.toFormGroup([
      this.$search,
      this.$fields,
    ]);

    // this.loadDataTable();
    // this.search();
    // if (!this.dataSource.paginator && this.dformPagination) {
    //     this.dataSource.paginator = this.dformPagination.paginator;
    // }
  }

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;
    this.search()
  }

  search() {
    if (!this.commonCategoryCode) {
      return
    }
    const searchParams = {}

    searchParams[this.form.get('fields').value] = this.form.get('search').value || ''

    const options: HttpOptions = {
      url: environment.hostApi,
      path: `${PATH.LIST}`,
      params: {
        ...searchParams,
        commonCategoryCode: this.commonCategoryCode,
        page: this.pageIndex,
        size: this.pageSize,
        status: 1
      }
    };

    this.indicator.showActivityIndicator();
    try {
      this.categoryPublic.getListCategory(options).pipe(
        takeUntil(this.ngUnsubscribe),
        finalize(() => this.indicator.hideActivityIndicator())
      ).subscribe((res: any) => {
        if (res) {
          this.totalItem = res.content.totalElements;
          const page = this.pageIndex * this.pageSize;
          this.dataTable = res.content.content.map((x, index) => {
            return {...x, stt: page + index + 1};
          });
          this.dataSource = new MatTableDataSource(this.dataTable);
        }
      });
    } catch (e) {
      debugger
    }
  }

  loadDataTable() {
    this.totalItem = this.dataTable.length;
    this.pageSize = 5;
    this.dataSource = new MatTableDataSource(this.dataTable);
  }

  create() {
    this.dialogService.componentDialog(CreateCategoryComponent, {
      width: '60%',
      data: { commonCategoryCode : this.commonCategoryCode}
    }, (res) => {
      if (res) {
        this.dataTable.push({stt: this.dataTable.length + 1, ...res})
        this.loadDataTable();

        this.indicator.showActivityIndicator();
        this.categoryPublic.addCategory(res).pipe(
          takeUntil(this.ngUnsubscribe),
          finalize(() => this.indicator.hideActivityIndicator())
        ).subscribe((res: any) => {
          if (res) {
            const config = {
              positionClass: 'toast-top-right',
              extendedTimeOut: 1000
            };
            this._toastService.showToastr("Thông báo", "Thêm danh mục thành công", MessageSeverity.success, config)
            search()
          }
        });
      }
    });
  }

  update(element) {
    this.dialogService.componentDialog(UpdateCategoryComponent, {
      width: '60%',
      data: {
        element
      }
    }, (res) => {
      if (res) {
        this.indicator.showActivityIndicator();
        debugger
        this.categoryPublic.updateCategory(element.id, {...element,...res}).pipe(
          takeUntil(this.ngUnsubscribe),
          finalize(() => this.indicator.hideActivityIndicator())
        ).subscribe((res: any) => {
          if (res) {
            const config = {
              positionClass: 'toast-top-right',
              extendedTimeOut: 1000
            };
            this._toastService.showToastr("Thông báo", "Sửa danh mục thành công", MessageSeverity.success, config)

            this.search()
          }
        });
      }
    });
  }

  delete(element) {
    this.dialogService.confirm({ title: 'Xác nhận', message: 'Bạn chắc chắn muốn xóa bản ghi này?'}, (result => {
      if (result) {
        this.categoryPublic.deleteCategory(element.id).pipe(
          takeUntil(this.ngUnsubscribe),
          finalize(() => this.indicator.hideActivityIndicator())
        ).subscribe((res: any) => {
          if (res) {
            const config = {
              positionClass: 'toast-top-right',
              extendedTimeOut: 1000
            };
            this._toastService.showToastr("Thông báo", "Xóa danh mục thành công", MessageSeverity.success, config)

            this.search()
          }
        });
      }
    }))
  }

  /**
   * Lấy text trạng thái của người dùng
   * @param element
   */
  status(element: any): string {
    if (!element && !element.isActive) {
      return;
    }
    const currentStatus = CATEGORY_STATUS.find(f => f?.key == element.isActive);
    return `<label class="wf-status ${currentStatus?.class}">${currentStatus?.value}</label>`;
  }

  onSelectCategory($event: string) {
    this.commonCategoryCode = $event
    this.search()
  }

}
