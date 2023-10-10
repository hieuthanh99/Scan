import {
    Component,
    Inject,
    Injector
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';
import { UpdateCategoryComponent } from '../../category-public/update-category/update-category.component';
import { key, name } from '../data-form/category-resource-edit';
@Component({
    selector: 'app-category-resource-create',
    templateUrl: './category-resource-create.component.html',
    styleUrls: ['./category-resource-create.component.scss']
})
export class CategoryResourceCreateComponent extends ComponentDialogAbstract {
    $key = key();
    $name = name()
    constructor(protected injector: Injector,
        public dialogRef: MatDialogRef<UpdateCategoryComponent>,
        @Inject(MAT_DIALOG_DATA) private dataDialog: any) {
        super(injector);
    }

    protected initData(): void {
        this.form = this.itemControl.toFormGroup([this.$key, this.$name]);
    }

    closeDialog() {
        if (this.dialogRef.close) { this.dialogRef.close(null); }
    }

    saveData() {
        const formData = this.form.getRawValue();
        this.dialogRef.close(formData)
    }
}