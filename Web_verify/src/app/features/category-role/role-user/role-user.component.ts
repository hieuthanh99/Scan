import {
    Component,
    ElementRef,
    EventEmitter,
    Injector,
    Input,
    Output,
    ViewChild
} from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import {
    PageEvent
} from '@angular/material/paginator';
import {
    MatTableDataSource
} from '@angular/material/table';
import { environment } from '@env/environment';
import {
    ComponentAbstract, HttpOptions
} from '@shared-sm';
import { finalize, takeUntil } from 'rxjs';
import { ExcelUtils } from '../../category-user/utils/excel.utils';
import {
    PATH, ROLE_APPROVER, ROLE_MAKER
} from '../constants';
import {
    username,
} from '../../category-user/data-form/category-user';
import { CategoryRoleService } from '../services/category-role.service';
@Component({
    selector: 'app-role-user',
    templateUrl: './role-user.component.html',
    styleUrls: ['./role-user.component.scss']
})
export class RoleUserComponent extends ComponentAbstract {
    @Input() listRight: Array<any> = [];
    $username = username();
    checkAll: boolean;
    displayedColumns = ['checkbox', 'stt', 'code', 'name', 'desciption'];
    @ViewChild('file') myInputVariable!: ElementRef;
    pageEvent: PageEvent = new PageEvent();
    public pageSize = 5;
    public pageIndex = 0;
    public totalItem = 0;
    searchModel: string;
    isUploadSuccess = false;
    SHEET_NAME = 'Template Full';
    panelOpenState = false;
    searchText = '';
    @Output() onChangeUser: EventEmitter<any> = new EventEmitter<any>();
    form = new FormGroup({
        search: new FormControl(''),
    });
    dataTable = [
        {
            checked: true,
            stt: '1',
            code: '01_VIEW',
            name: 'Xem thông tin',
            desciption: '',
        },
        {
            checked: true,
            stt: '2',
            code: '02_CREATE',
            name: 'Tạo mới thông tin',
            desciption: '',
        },
        {
            checked: true,
            stt: '3',
            code: '03_EDIT',
            name: 'Cập nhật thông tin',
            desciption: '',
        },
        {
            checked: true,
            stt: '4',
            code: '04_DELETE',
            name: 'Xóa thông tin',
            desciption: '',
        },
        {
            stt: '5',
            code: '05_IMPORT',
            name: 'Import dữ liệu',
            desciption: '',
        },
        {
            stt: '6',
            code: '06_UPLOAD',
            name: 'Đẩy lên dữ liệu',
            desciption: '',
        },
    ];
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

    search() {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: PATH.LIST,
            params: {
            }
        };
        this.indicator.showActivityIndicator();
        this.categoryRoleService.getListRole(this.pageIndex, this.pageSize, this.searchText).pipe(
            takeUntil(this.ngUnsubscribe),
            finalize(() => this.indicator.hideActivityIndicator())
        ).subscribe((res: any) => {
            if (res) {
                this.totalItem = res.data.totalCount;
                const page = this.pageIndex * this.pageSize;
                this.dataTable = res.data.items.map((x, index) => {
                    return { ...x, stt: page + index + 1 };
                });
                this.dataSource = new MatTableDataSource(this.dataTable);
            }
        });
    }

    componentInit(): void {
        this.form = this.itemControl.toFormGroup([this.$username, this.$username]);
        this.dataSource = new MatTableDataSource(this.dataTable);
        this.totalItem = this.dataTable.length;
        this.pageSize = 5;
        if (!this.dataSource.paginator && this.dformPagination) {
            this.dataSource.paginator = this.dformPagination.paginator;
        }
    };

    onClickSearch($event?: string) {
        // let $search = this.accountList;
        // if (!!$event) {
        //   $search = this.accountList.filter((val: Account) => val.accNumber.includes($event) || val.accName.includes($event));
        // }
        // this._group = this.groupBy($search.map((element: any, index) => {
        //   return { ...element, no: index + 1 };
        // }));
        // this.loadTable(this._group);
        // this.totalItem = this._group.length;
    }


    loadDataTable() {
        this.totalItem = this.dataTable.length;
        this.pageSize = 5;
        this.dataSource = new MatTableDataSource(this.dataTable);
    }

    _onCheckBoxAll($event) {
        if ($event.checked) {
            //this.listRight = this.dataTable.map((val) => ({ id: val.id, allow: true }));
        } else {
            this.listRight = [];
        }
    }

    _onCheckBox($event: MatCheckboxChange, id: number) {
        const right = { id, allow: true }
        if ($event.checked) {
            this.listRight?.push(right);
        } else {
            this.listRight = this.listRight?.filter((val) => val.id !== id);
        }
        this.onChangeUser.emit(this.listRight);
    }
}