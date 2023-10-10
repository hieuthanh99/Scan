import {
    Component,
    Inject,
    Injector
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';
import { key, name } from '../data-form/category-user-edit';
@Component({
    selector: 'app-category-user-create',
    templateUrl: './category-user-create.component.html',
    styleUrls: ['./category-user-create.component.scss']
})
export class CategoryUserCreateComponent  extends ComponentDialogAbstract {
    $key = key();
    $name = name()
    constructor(protected injector: Injector,
        public dialogRef: MatDialogRef<CategoryUserCreateComponent>,
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