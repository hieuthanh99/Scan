import {
    Component,
    Inject,
    Injector
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import {
    ComponentAbstract
} from '@shared-sm';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';
import {
  code, commonCategoryCode, description, isDefault, name, orderNum, value,
} from '../data-form/create-category';
import { UpdateCategoryComponent } from '../update-category/update-category.component';
import {
    ROLE_APPROVER, ROLE_MAKER
} from './../constants';
@Component({
    selector: 'app-create-category',
    templateUrl: './create-category.component.html',
    styleUrls: ['./create-category.component.scss']
})
export class CreateCategoryComponent extends ComponentDialogAbstract {

  $code = code();
  $name = name();
  $value = value();
  $commonCategoryCode = commonCategoryCode();
  $orderNum = orderNum();
  $isDefault = isDefault();
  $description = description();

    constructor(protected injector: Injector,
        public dialogRef: MatDialogRef<UpdateCategoryComponent>,
        @Inject(MAT_DIALOG_DATA) private dataDialog: any) {
        super(injector);
    }

    protected initData(): void {
        this.$commonCategoryCode.value = this.dataDialog.commonCategoryCode
        this.form = this.itemControl.toFormGroup([
          this.$code,
          this.$name,
          this.$value,
          this.$commonCategoryCode,
          this.$orderNum,
          this.$isDefault,
          this.$description,
        ]);
    }

    closeDialog() {
        if (this.dialogRef.close) { this.dialogRef.close(null); }
    }

    saveData() {
        if (this.form.valid)
        {
          const formData = this.form.getRawValue();
          this.dialogRef.close(formData)
        }
    }
}
