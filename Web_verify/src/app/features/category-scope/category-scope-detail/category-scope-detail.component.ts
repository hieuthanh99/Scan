import {
    Component,
    Inject,
    Injector
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';
import {
    id, code, description, name
} from '../data-form/category-scope-detail.';
@Component({
    selector: 'app-category-scope',
    templateUrl: './category-scope-detail.component.html',
    styleUrls: ['./category-scope-detail.component.scss']
})
export class CategoryScopeDetailComponent extends ComponentDialogAbstract {
    $id = id();
    $code = code();
    $name = name();
    $description = description()
    constructor(protected injector: Injector,
        public dialogRef: MatDialogRef<CategoryScopeDetailComponent>,
        @Inject(MAT_DIALOG_DATA) public dataDialog: any) {
        super(injector);
    }
    saveData() {
        this.validateAllFields(this.form);
        if (!this.form.valid) {
            this.dialogService.error({
                title: 'dialog.notification',
                message: 'dialog.valid-error',
            }, res => {
                if (res) {
                }
            });
            return;
        }
        const formData = this.form.getRawValue();
        this.dialogRef.close(formData)
    }

    protected initData(): void {
        this.form = this.itemControl.toFormGroup([this.$id, this.$code, this.$name, this.$description]);
        console.log('this.dataDialog?.element', this.dataDialog?.element);
        this.form.patchValue(this.dataDialog?.element);
        if (this.dataDialog?.isView) {
            this.form.disable();
        }
    }
    closeDialog() {
        if (this.dialogRef.close) 
        { 
            this.dialogRef.close(null); 
        }
    }
}