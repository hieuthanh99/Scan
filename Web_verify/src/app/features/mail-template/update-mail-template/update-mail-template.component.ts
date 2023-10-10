import {
    Component,
    Inject,
    Injector
} from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentDialogAbstract } from 'src/app/shared/abstract/component-dialog.abstract';
import { body, description, subject, templateId } from '../data-form/update-mail-template';
import { FORM_STATUS } from '../constants';

@Component({
    selector: 'app-update-mail-template',
    templateUrl: './update-mail-template.component.html',
    styleUrls: ['./update-mail-template.component.scss']
})
export class UpdateMailTemplateComponent extends ComponentDialogAbstract {
    $templateId = templateId();
    $subject = subject();
    $body = body();
    $description = description();
    
    constructor(protected injector: Injector,
        public dialogRef: MatDialogRef<UpdateMailTemplateComponent>,
        @Inject(MAT_DIALOG_DATA) private dataDialog: any) {
        super(injector);
    }

    protected initData(): void {
        this.form = this.itemControl.toFormGroup([
            this.$templateId,
            this.$subject,
            this.$body,
            this.$description,
        ]);

        this.form.patchValue(this.dataDialog?.element);
    }

    closeDialog() {
        if (this.dialogRef.close) { this.dialogRef.close(null); }
    }

    saveData() {
        if(this.form.status === FORM_STATUS.VALID) {
            const formData = this.form.getRawValue();
            this.dialogRef.close(formData);
        }
    }
}
