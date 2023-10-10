import {
    Component,
    ElementRef,
    Injector,
    ViewChild
} from '@angular/core';
import {
    PageEvent
} from '@angular/material/paginator';
import {
    MatTableDataSource
} from '@angular/material/table';
import { environment } from '@env/environment';
import {
    ComponentAbstract, DformPaginationPlusComponent, HttpOptions, MessageSeverity, Status
} from '@shared-sm';
import { finalize, takeUntil } from 'rxjs';
import { IConfirmModel } from 'src/app/shared/dform-dialogs';
import { ExcelUtils } from '../../category-user/utils/excel.utils';
import { CategoryScopeDetailComponent } from '../category-scope-detail/category-scope-detail.component';
import {
    PATH, ROLE_APPROVER, ROLE_MAKER
} from '../constants';
import {
    code,
    name
} from '../data-form/category-scope';
import { CategoryScopeService } from '../services/category-scope.service';
@Component({
    selector: 'app-category-scope',
    templateUrl: './category-scope.component.html',
    styleUrls: ['./category-scope.component.scss']
})
export class CategoryScopeComponent extends ComponentAbstract {
    $code = code();
    $name = name();
    displayedColumns = ['stt', 'code', 'name', 'desciption', 'action'];
    @ViewChild('file') myInputVariable!: ElementRef;
    isUploadSuccess = false;
    panelOpenState = false;
    SHEET_NAME = 'Template Full';
    searchText = ''
    btnUpload = [
        {
            key: 'import',
            name: 'Upload file',
            icon: 'mbb-icon ic-upload'
        },
        {
            key: 'export',
            name: 'Tải file mẫu',
            icon: 'mbb-icon ic-download'
        }
    ]
    constructor(
        protected injector: Injector,
        private excelUtils: ExcelUtils,
        private categoryScopeService: CategoryScopeService) {
        super(injector);
        this.initRole(ROLE_MAKER, ROLE_APPROVER);
    }

    componentInit(): void {
        this.form = this.itemControl.toFormGroup([this.$code, this.$name]);
        this.search();
    };

    search() {
        this.indicator.showActivityIndicator();
        this.categoryScopeService.getListScope(this.pageIndex, this.pageSize, this.searchText).pipe(
            takeUntil(this.ngUnsubscribe),
            finalize(() => this.indicator.hideActivityIndicator())
        ).subscribe((res: any) => {
            if (res && res.base && res.base.code === Status.SUCCESS && res.content) {
                this.hasDataSource = true;
                const page = this.pageIndex * this.pageSize;
                const data = res.content.content.sort((a, b) => { return <any>new Date(b.createdDate) - <any>new Date(a.createdDate); }).map((obj, index) => {
                    obj.stt = page + index + 1;
                    return obj;
                });
                this.loadDataTable(data, res.content.totalElements);
            } else {
                this.hasDataSource = false;
            }

        });
    }

    loadDataTable(dataSource, totalItem) {
        this.dataSource = new MatTableDataSource(dataSource);
        if (!this.dataSource.paginator && this.dformPagination) {
            this.dataSource.paginator = this.dformPagination.paginator;
        }
        this.totalItem = totalItem;
    }

    create() {
        this.dialogService.componentDialog(CategoryScopeDetailComponent, {
            width: '60%',
            data: {
                isCreate: true,
                title: 'Thêm mới thao tác'
            }
        }, (res) => {
            if (res) {
                console.log('res', res);
                this.indicator.showActivityIndicator();
                this.categoryScopeService.createScope(res).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    if (res && res.base && res.base.code === Status.SUCCESS) {
                        this.search();
                        this.toastr.showToastr(
                            'Tạo mới thông tin thao tác thành công!',
                            'Thông báo!',
                            MessageSeverity.success
                        );
                    }
                });
            }
        });
    }

    update(element) {
        this.dialogService.componentDialog(CategoryScopeDetailComponent, {
            width: '60%',
            data: {
                isCreate: false,
                element,
                title: 'Cập nhật thao tác'
            }
        }, (res) => {
            if (res) {
                this.indicator.showActivityIndicator();
                this.categoryScopeService.updateScope(res).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    if (res && res.base && res.base.code === Status.SUCCESS) {
                        this.search();
                        this.toastr.showToastr(
                            'Cập nhật thông tin thao tác thành công!',
                            'Thông báo!',
                            MessageSeverity.success
                        );
                    }
                });
            }
        });
    }

    view(element) {
        this.dialogService.componentDialog(CategoryScopeDetailComponent, {
            width: '60%',
            data: {
                element,
                isView: true,
                title: 'Thông tin thao tác'
            }
        }, (res) => {
            if (res) {
            }
        });
    }

    delete(element): void {
        this.dialogService.confirm({
            title: "Nhắc nhở",
            message: `Bạn muốn xóa thao tác"${element.name}"?`
        }, (data: any) => {
            if (data) {
                this.categoryScopeService.deleteScope(element.id).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    console.log('res', res);
                    if (res && res.base && Status.SUCCESS) {
                        this.search();
                        this.toastr.showToastr(
                            'Đã xóa thao tác thành công!',
                            'Thông báo!',
                            MessageSeverity.success
                        );
                    }
                });
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