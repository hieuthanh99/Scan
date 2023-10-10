import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, Injector } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ComponentAbstract } from '@shared-sm';
import { finalize, takeUntil } from 'rxjs';
import { CategoryResourceCreateComponent } from '../category-resource-create/category-resource-create.component';
import { CategoryResourceDetailComponent } from '../category-resource-detail/category-resource-detail.component';
import { CategoryResourceEditComponent } from '../category-resource-edit/category-resource-edit.component';
import { name } from '../data-form/category-resource';
import { IResource, TreeNode } from '../models/category-resource.model';
import { CategoryDataSource } from '../services/category-resource.datasource';
import { CategoryResourceService } from '../services/category-resource.service';
import { ROLE_APPROVER, ROLE_MAKER } from './../constants';
@Component({
    selector: 'app-category-resource',
    templateUrl: './category-resource.component.html',
    styleUrls: ['./category-resource.component.scss']
})
export class CategoryResourceComponent extends ComponentAbstract {
    $name = name()
    displayedColumns = ['name', 'key', 'url', 'modifiedDate', 'action'];
    panelOpenState = false;
    dataTable: IResource[] = [
        {
            id: 1,
            name: 'data test',
            key: 'data-test',
            url: '/data-test',
            createdDate: '10/05/2023',
            createdBy: 'anhdkn.os',
            modifiedDate: '11/05/2023',
            modifiedBy: 'anhdkn.os',
            hasChild: true,
        },
        {
            id: 2,
            name: 'SM',
            key: 'sm',
            url: '/sm',
            createdDate: '12/05/2023',
            createdBy: 'anhdkn.os',
            hasChild: false,
        },
        {
            id: 5,
            name: 'SM',
            key: 'sm',
            url: '/sm',
            createdDate: '12/05/2023',
            createdBy: 'anhdkn.os',
            hasChild: true,
        },
        {
            id: 6,
            name: 'SM',
            key: 'sm',
            url: '/sm',
            createdDate: '12/05/2023',
            createdBy: 'anhdkn.os',
            hasChild: false,
        },
        {
            id: 7,
            name: 'SM',
            key: 'sm',
            url: '/sm',
            createdDate: '12/05/2023',
            createdBy: 'anhdkn.os',
            hasChild: false,
        },
        {
            id: 8,
            name: 'SM',
            key: 'sm',
            url: '/sm',
            createdDate: '12/05/2023',
            createdBy: 'anhdkn.os',
            hasChild: false,
        },
    ];
    
    constructor(protected injector: Injector, private categoryresource: CategoryResourceService) {
        super(injector);
        this.initRole(ROLE_MAKER, ROLE_APPROVER);
    }

    componentInit(): void {
        this.form = this.itemControl.toFormGroup([this.$name]);
        this.loadDataTable();
        // if (this.dataSource && !this.dataSource.paginator && this.dformPagination) {
        //     this.dataSource.paginator = this.dformPagination.paginator;
        // }
    }

    /**
     * Tìm kiếm
     */
    search(): void {

    }

    treeControl: FlatTreeControl<TreeNode>;

    treeDataSource: CategoryDataSource;

    getLevel = (node: TreeNode) => node.level;

    isExpandable = (node: TreeNode) => node.expandable;

    hasChild = (_: number, _nodeData: TreeNode) => _nodeData.expandable;
    loadDataTable() {
        this.totalItem = this.dataTable.length;
        this.pageSize = 5;
        // this.dataSource = new MatTableDataSource(this.dataTable);
        this.treeControl = new FlatTreeControl<TreeNode>(this.getLevel, this.isExpandable);
        this.treeDataSource = new CategoryDataSource(this.treeControl, this.categoryresource);
        this.treeDataSource.data = this.dataTable.map(element => {
            return {
                item: element,
                level: 0,
                expandable: element.hasChild,
                isLoading : false,
            };
        });
    }

    /**
     * Ngày thay đổi cuối cùng của hệ thống
     * @param element 
     */
    lastChange(element: IResource): string {
        return element.modifiedDate ? element.modifiedDate : element.createdDate;
    }

    /**
     * Hiển thị dialog tạo mới hệ thống
     */
    create(): void {
        this.dialogService.componentDialog(CategoryResourceCreateComponent, {
            width: '60%',
            data: {
            }
        }, (res) => {
            if (res) {
                this.dataTable.push({ stt: this.dataTable.length + 1, ...res })
                this.loadDataTable();
                this.indicator.showActivityIndicator();
                this.categoryresource.create(res).pipe(
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
    detail(element: IResource): void {
        this.dialogService.componentDialog(CategoryResourceDetailComponent, {
            width: '60%',
            data: {
                item: element
            }
        }, (res) => {});
    }

    /**
     * Hiển thị dialog cập nhật hệ thống
     * @param element Hệ thống muốn cập nhật thông tin
     */
    update(element: IResource): void {
        this.dialogService.componentDialog(CategoryResourceEditComponent, {
            width: '60%',
            data: {
                element
            }
        }, (res) => {
            if (res) {
                // this.indicator.showActivityIndicator();
                // this.categoryresource.updateCategory(res).pipe(
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
    delete(element: IResource): void {
        this.dialogService.confirm({
            title: "Nhắc nhở",
            message: `Bạn muốn xóa hệ thống "${element.name}"?`
        }, (res) => {
            if (res) {

            }
        });
    }
}