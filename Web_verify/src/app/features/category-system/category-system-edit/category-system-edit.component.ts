import {
    Component,
    Inject,
    Injector
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';
import { UpdateCategoryComponent } from '../../category-public/update-category/update-category.component';
import { appCode, appName, clientId, elementName, isKeycloak, isShowMenu, secretKey, status, subDomain, type, description } from '../data-form/category-system-edit';
@Component({
    selector: 'app-category-system-edit',
    templateUrl: './category-system-edit.component.html',
    styleUrls: ['./category-system-edit.component.scss']
})
export class CategorySystemEditComponent extends ComponentDialogAbstract {
    id: string;

    $appCode = appCode();
    $appName = appName();
    $isKeycloak = isKeycloak();
    $secretKey = secretKey();
    $status = status();
    $clientId = clientId();
    $isShowMenu = isShowMenu();
    $type = type();
    $subDomain = subDomain();
    $elementName = elementName();
    $description = description();
    $image;

    constructor(protected injector: Injector,
        public dialogRef: MatDialogRef<UpdateCategoryComponent>,
        @Inject(MAT_DIALOG_DATA) private dataDialog: any) {
        super(injector);
    }

    protected initData(): void {
        this.form = this.itemControl.toFormGroup([this.$appCode, this.$appName, this.$isKeycloak, this.$secretKey, this.$status, this.$clientId, this.$isShowMenu, this.$type, this.$subDomain, this.$elementName, this.$description]);
        this.form.patchValue(this.dataDialog?.element)
        this.id = this.dataDialog?.element.id;
    }

    closeDialog() {
        if (this.dialogRef.close) { this.dialogRef.close(null); }
    }

    saveData() {
        if (this.form.valid) {
            const formData = this.form.getRawValue();
            this.dialogRef.close({
                id: this.id,
                ...formData
            })
        }
    }
}