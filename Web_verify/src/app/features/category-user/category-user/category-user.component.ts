import { Component, ElementRef, Injector, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ComponentAbstract } from '@shared-sm';
import { finalize, takeUntil } from 'rxjs';
import { CategoryUserCreateComponent } from '../category-user-create/category-user-create.component';
import { CategoryUserEditComponent } from '../category-user-edit/category-user-edit.component';
import { username } from '../data-form/category-user';
import IUser from '../models/category-user.model';
import { CategoryUserService } from '../services/category-user.service';
import { ExcelUtils } from '../utils/excel.utils';
import { ROLE_APPROVER, ROLE_MAKER, USER_STATUS } from './../constants';
@Component({
    selector: 'app-category-user',
    templateUrl: './category-user.component.html',
    styleUrls: ['./category-user.component.scss']
})
export class CategoryUserComponent extends ComponentAbstract {
    @ViewChild('file') myInputVariable!: ElementRef;
    isUploadSuccess = false;
    SHEET_NAME = 'Template Full';

    $name = username()
    displayedColumns = ['stt', 'username', 'name', 'siteCode', 'status', 'modifiedDate', 'action'];
    dataTable: IUser[] = [
        {
            stt: 1,
            id: 1,
            username: 'username1',
            name: 'data test',
            email: 'username1@demo.com',
            phone: '0987654321',
            siteCode: 'VN001005',
            status: 'D',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
        {
            stt: 2,
            id: 2,
            username: 'username2',
            name: 'data test',
            email: 'username1@demo.com',
            phone: '0987654321',
            siteCode: 'VN001001',
            status: 'A',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
        {
            stt: 3,
            id: 3,
            username: 'username3',
            name: 'data test',
            email: 'username1@demo.com',
            phone: '0987654321',
            siteCode: 'VN001005',
            status: 'A',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
        {
            stt: 4,
            id: 4,
            username: 'username4',
            name: 'data test',
            email: 'username4@demo.com',
            phone: '0987654321',
            siteCode: 'VN001003',
            status: 'A',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
        {
            stt: 5,
            id: 5,
            username: 'username5',
            name: 'data test',
            email: 'username1@demo.com',
            phone: '0987654321',
            siteCode: 'VN001005',
            status: 'A',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
        {
            stt: 6,
            id: 6,
            username: 'username1',
            name: 'data test',
            email: 'username1@demo.com',
            phone: '0987654321',
            siteCode: 'VN001005',
            status: 'A',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
        {
            stt: 7,
            id: 7,
            username: 'username7',
            name: 'data test',
            email: 'username7@demo.com',
            phone: '0987654321',
            siteCode: 'VN001005',
            status: 'A',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
        {
            stt: 8,
            id: 8,
            username: 'username8',
            name: 'data test',
            email: 'username1@demo.com',
            phone: '0987654321',
            siteCode: 'VN001003',
            status: 'A',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
        {
            stt: 9,
            id: 9,
            username: 'username9',
            name: 'data test',
            email: 'username1@demo.com',
            phone: '0987654321',
            siteCode: 'VN001002',
            status: 'A',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
        {
            stt: 10,
            id: 10,
            username: 'username10',
            name: 'data test',
            email: 'username1@demo.com',
            phone: '0987654321',
            siteCode: 'VN001001',
            status: 'A',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os'
        },
    ];
    panelOpenState = false;
    constructor(
        protected injector: Injector,
        private categorysystem: CategoryUserService,
        private excelUtils: ExcelUtils
    ) {
        super(injector);
        this.initRole(ROLE_MAKER, ROLE_APPROVER);
    }

    componentInit(): void {
        this.form = this.itemControl.toFormGroup([this.$name]);
        this.loadDataTable();
        if (this.dataSource && !this.dataSource.paginator && this.dformPagination) {
            this.dataSource.paginator = this.dformPagination.paginator;
        }
    }

    /**
     * Tìm kiếm
     */
    search(): void {

    }

    loadDataTable() {
        this.totalItem = this.dataTable.length;
        this.pageSize = 5;
        this.dataSource = new MatTableDataSource(this.dataTable);
    }

    /**
     * Ngày thay đổi cuối cùng của hệ thống
     * @param element 
     */
    lastChange(element: IUser): string {
        return element.modifiedDate ? element.modifiedDate : element.createdDate;
    }

    /**
     * Lấy text trạng thái của người dùng
     * @param element 
     */
    status(element: IUser): string {
        if (!element && !element.status) {
            return;
        }
        const currentStatus = USER_STATUS.find(f => f?.key === element.status);
        return `<label class="wf-status ${currentStatus?.class}">${currentStatus?.value}</label>`;
    }

    /**
     * Hiển thị dialog tạo mới hệ thống
     */
    create(): void {
        this.dialogService.componentDialog(CategoryUserCreateComponent, {
            width: '60%',
            data: {
            }
        }, (res) => {
            if (res) {
                this.dataTable.push({ stt: this.dataTable.length + 1, ...res })
                this.loadDataTable();
                this.indicator.showActivityIndicator();
                this.categorysystem.create(res).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    if (res) {
                    }
                });
            }
        });
    }

    /**
     * Hiển thị dialog chi tiết hệ thống
     * @param element Hệ thống muốn hiển thị
     */
    detail(element: IUser): void {
        this.dialogService.componentDialog(CategoryUserEditComponent, {
            width: '60%',
            data: {
                element,
                isEdit: false
            }
        }, (res) => {
            if (res) {
                // this.indicator.showActivityIndicator();
                // this.categoryPublic.updateCategory(res).pipe(
                //     takeUntil(this.ngUnsubscribe),
                //     finalize(() => this.indicator.hideActivityIndicator())
                // ).subscribe((res: any) => {
                //     if (res) {
                //     }
                // });
            }
        });
    }

    /**
     * Hiển thị dialog cập nhật hệ thống
     * @param element Hệ thống muốn cập nhật thông tin
     */
    update(element: IUser): void {
        this.dialogService.componentDialog(CategoryUserEditComponent, {
            width: '60%',
            data: {
                element,
                isEdit: true
            }
        }, (res) => {
            if (res) {
                // this.indicator.showActivityIndicator();
                // this.categoryPublic.updateCategory(res).pipe(
                //     takeUntil(this.ngUnsubscribe),
                //     finalize(() => this.indicator.hideActivityIndicator())
                // ).subscribe((res: any) => {
                //     if (res) {
                //     }
                // });
            }
        });
    }

    /**
     * Xóa hệ thống
     * @param element Hệ thống muốn xóa
     */
    delete(element: IUser): void {
        this.dialogService.confirm({
            title: "Nhắc nhở",
            message: `Bạn muốn xóa người dùng "${element.name}"?`
        }, (res) => {
            if (res) {

            }
        });
    }

    /**
   * Chọn file
   */
    handleFileInput(event?: any): void {
        const lengthListFile = event.target.files.length;
        if (lengthListFile) {
            const fileName = (event.target.files[0] as File)?.name;
            if (!fileName.toLowerCase().includes('.xlsx') && !fileName.toLowerCase().includes('.xls')) {
                this.isUploadSuccess = false;
                this.dialogService.error({
                    title: 'Thông báo',
                    innerHTML: 'Vui lòng chọn đúng định đạng File xlsx hoặc xls',
                }, res => {
                    if (res) {
                    }
                });
                return;
            }

            this.excelUtils.readExcelFile(event, this.SHEET_NAME).then(data => {
                console.log(data);
            });
        }
    }
}