import {
    Component,
    Inject,
    Injector
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';
import {
    id, code, description, name
} from '../data-form/category-role-detail';
@Component({
    selector: 'app-category-role-detail',
    templateUrl: './category-role-detail.component.html',
    styleUrls: ['./category-role-detail.component.scss']
})
export class CategoryRoleDetailComponent extends ComponentDialogAbstract {
    $id = id();
    $code = code();
    $name = name();
    $description = description();
    listRight;
    constructor(protected injector: Injector,
        public dialogRef: MatDialogRef<CategoryRoleDetailComponent>,
        @Inject(MAT_DIALOG_DATA) public dataDialog: any) {
        super(injector);
    }

    saveData() {
        const formData = this.form.getRawValue();
        this.dialogRef.close(formData)
    }

    protected initData(): void {
        this.form = this.itemControl.toFormGroup([this.$id, this.$code, this.$name, this.$description]);
        this.form.patchValue(this.dataDialog?.element);
        if (this.dataDialog?.isView) {
            this.form.disable();
        }
    }

    closeDialog() {
        if (this.dialogRef.close) { this.dialogRef.close(null); }
    }

    onChangeUser(listRights: Array<any>) {
        this.listRight = listRights;
        // this.saveLocal({rights: listRights});
    }
}