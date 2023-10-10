import {
    Component,
    Inject,
    Injector
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';
import {
    key, name
} from '../data-form/category-user-edit';
@Component({
    selector: 'app-category-user-edit',
    templateUrl: './category-user-edit.component.html',
    styleUrls: ['./category-user-edit.component.scss']
})
export class CategoryUserEditComponent extends ComponentDialogAbstract {
    $key = key();
    $name = name();
    isEdit = false;
    constructor(protected injector: Injector,
        public dialogRef: MatDialogRef<CategoryUserEditComponent>,
        @Inject(MAT_DIALOG_DATA) private dataDialog: any) {
        super(injector);
    }

    protected initData(): void {
        this.form = this.itemControl.toFormGroup([this.$key, this.$name]);
        this.form.patchValue(this.dataDialog?.element);
        this.isEdit = this.dataDialog?.isEdit;
        if (!this.dataDialog?.isEdit) {
            this.form.disable();
        }
    }

    closeDialog() {
        if (this.dialogRef.close) { this.dialogRef.close(null); }
    }

    saveData() {
        const formData = this.form.getRawValue();
        this.dialogRef.close(formData)
    }
}