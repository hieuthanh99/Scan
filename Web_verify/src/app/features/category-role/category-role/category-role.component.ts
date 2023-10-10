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
import {
    PATH, ROLE_APPROVER, ROLE_MAKER
} from '../constants';
import {
    code,
    name
} from '../data-form/category-role';
import { CategoryRoleService } from '../services/category-role.service';
import { CategoryScopeDetailComponent } from '../../category-scope/category-scope-detail/category-scope-detail.component';
import { CategoryRoleDetailComponent } from '../category-role-detail/category-role-detail.component';
@Component({
    selector: 'app-role',
    templateUrl: './category-role.component.html',
    styleUrls: ['./category-role.component.scss']
})
export class CategoryRoleComponent extends ComponentAbstract {
    $code = code();
    $name = name();
    displayedColumns = ['stt', 'name', 'desciption', 'action'];
    @ViewChild('file') myInputVariable!: ElementRef;
    pageEvent: PageEvent = new PageEvent();
    public pageSize = 5;
    public pageIndex = 0;
    public totalItem = 0;
    panelOpenState = false;
    isUploadSuccess = false;
    searchText = '';
    SHEET_NAME = 'Template Full';
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
        private categoryRoleService: CategoryRoleService) {
        super(injector);
        this.initRole(ROLE_MAKER, ROLE_APPROVER);
    }

    componentInit(): void {
        this.form = this.itemControl.toFormGroup([this.$code, this.$name]);
        this.search();
    };

    search() {
        this.indicator.showActivityIndicator();
        this.categoryRoleService.getListRole(this.pageIndex, this.pageSize, this.searchText).pipe(
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
        this.dialogService.componentDialog(CategoryRoleDetailComponent, {
            width: '60%',
            data: {
                isCreate: true,
                title: 'Thêm mới vai trò'
            }
        }, (res) => {
            if (res) {
                console.log('res', res);
                this.indicator.showActivityIndicator();
                this.categoryRoleService.createRole(res).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    if (res && res.base && res.base.code === Status.SUCCESS) {
                        this.search();
                        this.toastr.showToastr(
                            'Tạo mới vai trò thành công!',
                            'Thông báo!',
                            MessageSeverity.success
                        );
                    }
                });
            }
        });
    }

    update(element) {
        this.categoryRoleService.getRoleById(element.id).pipe(
            takeUntil(this.ngUnsubscribe),
            finalize(() => this.indicator.hideActivityIndicator())
        ).subscribe((res: any) => {
            console.log('getbyid', res);
            if (res && res.base && res.base.code === Status.SUCCESS) {
                let data = res.content;
                let elementNew = Object.assign({}, data);
                elementNew.code = data.name;
                elementNew.name = data.description;
                elementNew.description = data.attributes && data.attributes.description && data.attributes.description.length > 0 ? data.attributes.description[0] : '';
                this.dialogService.componentDialog(CategoryRoleDetailComponent, {
                    width: '60%',
                    data: {
                        isCreate: false,
                        element: elementNew,
                        title: 'Cập nhật vai trò'
                    }
                }, (res) => {
                    if (res) {
                        this.indicator.showActivityIndicator();
                        this.categoryRoleService.updateRole(res).pipe(
                            takeUntil(this.ngUnsubscribe),
                            finalize(() => this.indicator.hideActivityIndicator())
                        ).subscribe((res: any) => {
                            if (res && res.base && res.base.code === Status.SUCCESS) {
                                this.search();
                                this.toastr.showToastr(
                                    'Cập nhật thông tin vai trò thành công!',
                                    'Thông báo!',
                                    MessageSeverity.success
                                );
                            }
                        });
                    }
                });
            }
        });


    }

    delete(element): void {
        this.dialogService.confirm({
            title: "Nhắc nhở",
            message: `Bạn muốn xóa vai trò"${element.name}"?`
        }, (data: any) => {
            if (data) {
                this.categoryRoleService.deleteRole(element.id).pipe(
                    takeUntil(this.ngUnsubscribe),
                    finalize(() => this.indicator.hideActivityIndicator())
                ).subscribe((res: any) => {
                    console.log('res', res);
                    if (res && res.base && res.base.code == Status.SUCCESS) {
                        this.search();
                        this.toastr.showToastr(
                            'Đã xóa vai trò thành công!',
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