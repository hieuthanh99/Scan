import { Component, Injector } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ComponentAbstract, Status } from '@shared-sm';
import { finalize, takeUntil } from 'rxjs';
import { CategorySystemCreateComponent } from '../category-system-create/category-system-create.component';
import { CategorySystemEditComponent } from '../category-system-edit/category-system-edit.component';
import { name, status } from '../data-form/category-system';
import ISystem from '../models/category-system.model';
import { CategorySystemService } from '../services/category-system.service';
import { APPLICATION_STATUS, ROLE_APPROVER, ROLE_MAKER } from './../constants';
@Component({
    selector: 'app-category-system',
    templateUrl: './category-system.component.html',
    styleUrls: ['./category-system.component.scss']
})
export class CategorySystemComponent extends ComponentAbstract {
    $name = name()
    $status = status()
    displayedColumns = ['stt', 'appCode', 'appName', 'status', 'modifiedDate', 'action'];
    panelOpenState = false;
    dataTable: ISystem[];
    
    constructor(
        protected injector: Injector, 
        private categorysystem: CategorySystemService
    ) {
        super(injector);
        this.initRole(ROLE_MAKER, ROLE_APPROVER);
    }

    componentInit(): void {
        this.form = this.itemControl.toFormGroup([this.$name, this.$status]);
        this.search();
        if (this.dataSource && !this.dataSource.paginator && this.dformPagination) {
            this.dataSource.paginator = this.dformPagination.paginator;
        }
    }

    /**
     * Tìm kiếm
     */
    search(): void {
        this.indicator.showActivityIndicator();
        const formData = this.form.getRawValue();
        var appName = formData?.appName;
        var status = this.panelOpenState ? formData?.status : null;
        this.categorysystem.search(appName, status, this.pageIndex, this.pageSize).pipe(
            takeUntil(this.ngUnsubscribe),
            finalize(() => this.indicator.hideActivityIndicator())
        ).subscribe((res: any) => {
            if (res?.base?.code === Status.SUCCESS && res?.content) {
                this.hasDataSource = true;
                const page = this.pageIndex * this.pageSize;
                const data = res.content.content.map((obj, index) => {
                    obj.stt = page + index + 1;
                    return obj;
                });
                this.totalItem = res.content.totalElements;
                this.dataTable = data;
                this.loadDataTable();
            } else {
                this.hasDataSource = false;
            }
        });
    }

    loadDataTable() {
        this.totalItem = this.dataTable.length;
        this.dataSource = new MatTableDataSource(this.dataTable);
    }

    /**
     * Ngày thay đổi cuối cùng của hệ thống
     * @param element 
     */
    getStatus(element: ISystem): string {
        if (!element && !element.status) {
            return;
        }
        const currentStatus = APPLICATION_STATUS.find(f => f?.key === element.status);
        return `<label class="wf-status ${currentStatus?.class}">${currentStatus?.value}</label>`;
    }

    /**
     * Hiển thị dialog tạo mới hệ thống
     */
    create(): void {
        this.dialogService.componentDialog(CategorySystemCreateComponent, {
            width: '60%',
            data: {
            }
        }, (res) => {
            if (res) {
                this.indicator.showActivityIndicator();
                this.categorysystem.create(res).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    if (res?.base?.code === Status.SUCCESS) {
                        window.location.reload();
                    }
                });
            }
        });
    }

    /**
     * Hiển thị dialog cập nhật hệ thống
     * @param element Hệ thống muốn cập nhật thông tin
     */
    update(element: ISystem): void {
        this.dialogService.componentDialog(CategorySystemEditComponent, {
            width: '60%',
            data: {
                element
            }
        }, (res) => {
            if (res) {
                this.indicator.showActivityIndicator();
                this.categorysystem.update(res).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    if (res?.base?.code === Status.SUCCESS) {
                        window.location.reload();
                    }
                });
            }
        });
    }

    /**
     * Xóa hệ thống
     * @param element Hệ thống muốn xóa
     */
    delete(element: ISystem): void {
        this.dialogService.confirm({
            title: "Nhắc nhở",
            message: `Bạn muốn xóa hệ thống "${element.appCode}"?`
        }, (res) => {
            if (res) {

            }
        });
    }
}