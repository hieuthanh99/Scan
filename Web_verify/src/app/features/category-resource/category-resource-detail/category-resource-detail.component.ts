import { Component, Inject, Injector } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';
import { UpdateCategoryComponent } from '../../category-public/update-category/update-category.component';
import { IResource } from '../models/category-resource.model';
@Component({
    selector: 'app-category-resource-detail',
    templateUrl: './category-resource-detail.component.html',
    styleUrls: ['./category-resource-detail.component.scss']
})
export class CategoryResourceDetailComponent extends ComponentDialogAbstract {
    displayedColumns: string[] = ["key", "value"];
    keyProperties: string[] = ['name', 'key', 'url', 'createdDate', 'createdBy', 'modifiedDate', 'modifiedBy'];
    nameProperties = {
        name: 'Tên Resource',
        key: 'Key',
        url: 'URL', 
        createdDate: 'Ngày tạo Resource',
        createdBy: 'Người tạo',
        modifiedDate: 'Lần chỉnh sửa cuối cùng',
        modifiedBy: 'Người chỉnh sửa'
    };
    dataTable: any[] = [];

    constructor(
        protected injector: Injector,
        public dialogRef: MatDialogRef<UpdateCategoryComponent>,
        @Inject(MAT_DIALOG_DATA) private dataDialog: any
    ) {
        super(injector);
        const item = this.dataDialog?.item;
        for (const key in item) {
            if (this.keyProperties.includes(key)) this.dataTable.push({ key: this.nameProperties[key], value: item[key] });
        }
    }

    protected initData(): void {
        
    }

    closeDialog() {
        if (this.dialogRef.close) { this.dialogRef.close(null); }
    }
}