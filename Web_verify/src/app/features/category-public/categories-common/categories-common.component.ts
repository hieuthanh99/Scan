import {CategoryPublicService} from './../services/category-public.service';
import {UpdateCategoryComponent} from './../update-category/update-category.component';
import {
  Component,
  Injector, Output , EventEmitter
} from '@angular/core';
import {
  MatTableDataSource
} from '@angular/material/table';
import {
  ComponentAbstract, CURRENCY_APP_CODE, HttpOptions
} from '@shared-sm';
import {
  key,
  name
} from '../data-form/category-public';
import {
  PATH,
  ROLE_APPROVER, ROLE_MAKER
} from './../constants';
import {finalize, takeUntil} from 'rxjs';
import {environment} from '@env/environment';
import {CreateCategoryComponent} from "../create-category/create-category.component";

import ISystem from "../../category-system/models/category-system.model";
import {PageEvent} from "@angular/material/paginator";
import {fields, search} from '../data-form/category-common';

@Component({
  selector: 'app-categories-common',
  templateUrl: './categories-common.component.html',
  styleUrls: ['./categories-common.component.scss']
})
export class CategoriesCommonComponent extends ComponentAbstract {

  @Output() onSelected = new EventEmitter<string>();
  $search = search();
  $fields = fields();

  displayedColumns = ['stt', 'code', 'name'];
  dataTable: ISystem[] = []
  selectedRow  = null

  constructor(protected injector: Injector, private categoryPublic: CategoryPublicService) {
    super(injector);
    this.initRole(ROLE_MAKER, ROLE_APPROVER);
  }


  componentInit(): void {
    this.form = this.itemControl.toFormGroup([
      this.$search,
      this.$fields
    ]);
    this.search();
  }

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.pageSize = e.pageSize;
    this.pageIndex = e.pageIndex;
    this.search()
  }

  search() {
    const searchParams = {}

    searchParams[this.form.get('fields').value] = this.form.get('search').value || ''

    const options: HttpOptions = {
      url: environment.hostApi,
      path: PATH.CATEGORY,
      params: {
        ...searchParams,
        page: this.pageIndex,
        size: this.pageSize
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
          if (this.totalItem > 0) {
            this.selectedRow = this.dataTable[0]
            this.onSelected.emit(this.selectedRow.code)
          }
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
      data: {}
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
        this.categoryPublic.updateCategory(element.id, res).pipe(
            takeUntil(this.ngUnsubscribe),
            finalize(() => this.indicator.hideActivityIndicator())
        ).subscribe((res: any) => {
            if (res) {
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
            this.search()
          }
        });
      }
    }))
  }

  rowClick(row) {
    console.log(row)
    this.selectedRow = row
    this.onSelected.emit(row.code)
  }
}
