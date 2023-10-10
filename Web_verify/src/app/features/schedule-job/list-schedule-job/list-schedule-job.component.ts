import {
    Component,
    Injector
} from '@angular/core';
import {
    MatTableDataSource
} from '@angular/material/table';
import {
  ComponentAbstract, CURRENCY_APP_CODE, HttpOptions
} from '@shared-sm';
import {
  name, action, code, description, value, status
} from '../data-form/list-schedule-job';
import {
  JOB_STATUS,
  PATH,
  ROLE_APPROVER, ROLE_MAKER
} from './../constants';


import ISystem from "../../category-system/models/category-system.model";

import {PageEvent} from "@angular/material/paginator";
import {environment} from "@env/environment";

import {finalize, takeUntil} from "rxjs";

import {CreateScheduleJobComponent} from "../create-schedule-job/create-schedule-job.component";
import {UpdateScheduleJobComponent} from "../update-schedule-job/update-schedule-job.component";
import {ScheduleJobService} from "../services/schedule-job.service";

import {IJob} from "../models/job.model";
import {ListHistoryExecuteJobComponent} from "../list-history-execute-job/list-history-execute-job.component";

@Component({
    selector: 'app-list-schedule-job',
    templateUrl: './list-schedule-job.component.html',
    styleUrls: ['./list-schedule-job.component.scss']
})
export class ListScheduleJobComponent extends ComponentAbstract {

  $code = code();
  $name = name();
  $value = value();
  $status = status();
  $description = description();
  $action = action();


  displayedColumns = ['stt',
    'processId',
    'code',
    'name',
    'expression',
    'status',
    'startTime',
    'lastTimeExecute',
    'description',
    'action'
  ];

  dataTable: ISystem[] = []

  constructor(protected injector: Injector, private _service: ScheduleJobService) {
    super(injector);
    this.initRole(ROLE_MAKER, ROLE_APPROVER);
  }


  componentInit(): void {
    this.form = this.itemControl.toFormGroup([
      this.$code,
      this.$name,
      this.$value,
      this.$status,
      this.$description,
      this.$action,
    ]);

    // this.loadDataTable();
    this.search();
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
    const options: HttpOptions = {
      url: environment.hostApi,
      path: `${PATH.LIST}`,
      body: {
        page: this.pageIndex,
        size: this.pageSize
      }
    };

    this.indicator.showActivityIndicator();
    try {
      this._service.getList(options).pipe(
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
    this.dialogService.componentDialog(CreateScheduleJobComponent, {
      width: '60%',
      data: {}
    }, (res) => {
      if (res) {
        this.dataTable.push({stt: this.dataTable.length + 1, ...res})
        this.loadDataTable();

        this.indicator.showActivityIndicator();
        this._service.add(res).pipe(
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

  update(element) {
    this.dialogService.componentDialog(UpdateScheduleJobComponent, {
      width: '60%',
      data: {
        element
      }
    }, (res) => {
      if (res) {
        this.indicator.showActivityIndicator();
        this._service.update(element.id, res).pipe(
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
        this._service.delete(element.processId).pipe(
          takeUntil(this.ngUnsubscribe),
          finalize(() => this.indicator.hideActivityIndicator())
        ).subscribe((res: any) => {
          this.search()
        });
      }
    }))
  }

  start(element) {
    this.dialogService.confirm({ title: 'Xác nhận', message: 'Bạn chắc chắn muốn chạy tiến trình này?'}, (result => {
      if (result) {
        try {
          this._service.startJob(element.processId).pipe(
            takeUntil(this.ngUnsubscribe),
            finalize(() => this.indicator.hideActivityIndicator())
          ).subscribe((res: any) => {
            this.search()
          });
        } catch (e) {
          this.dialogService.error({title: 'Có lỗi xảy ra', message: e.message || ''})
        }

      }
    }))
  }


  detail(element) {
    this.dialogService.componentDialog(ListHistoryExecuteJobComponent, {
      width: '60%',
      data: {
        ...element
      }
    }, (res) => {

    });
  }

  stop(element) {
    this.dialogService.confirm({ title: 'Xác nhận', message: 'Bạn chắc chắn muốn tạm dừng tiến trình này?'}, (result => {
      if (result) {
        try {
          this._service.stopJob(element.processId).pipe(
            takeUntil(this.ngUnsubscribe),
            finalize(() => this.indicator.hideActivityIndicator())
          ).subscribe((res: any) => {
            this.search()
          });
        } catch (e) {
          this.dialogService.error({title: 'Có lỗi xảy ra', message: e.message || ''})
        }
      }
    }))
  }


  /**
   * Lấy text trạng thái của người dùng
   * @param element
   */
  status(element: IJob): string {
    if (!element && !element.status) {
      return;
    }
    const currentStatus = JOB_STATUS.find(f => f?.key == element.status);
    return `<label class="wf-status ${currentStatus?.class}">${currentStatus?.value}</label>`;
  }

}
