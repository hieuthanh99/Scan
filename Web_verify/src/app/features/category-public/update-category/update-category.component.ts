import {
    Component,
    Inject,
    Injector
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';

import {
  code,
  commonCategoryCode, description, isDefault,
  name, orderNum,
  value
} from "../data-form/update-category";
@Component({
    selector: 'app-update-category',
    templateUrl: './update-category.component.html',
    styleUrls: ['./update-category.component.scss']
})
export class UpdateCategoryComponent extends ComponentDialogAbstract {

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
        this.form = this.itemControl.toFormGroup([
          this.$code,
          this.$name,
          this.$value,
          this.$commonCategoryCode,
          this.$orderNum,
          this.$isDefault,
          this.$description,
          ]);
        this.form.patchValue(this.dataDialog?.element)
    }

    closeDialog() {
        if (this.dialogRef.close) { this.dialogRef.close(null); }
    }

    saveData() {
        const formData = this.form.getRawValue();
        this.dialogRef.close(formData)
    }
}
