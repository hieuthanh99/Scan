import {
  Component, Inject,
  Injector,
  ViewChild
} from '@angular/core';
import {
  ComponentAbstract, CURRENCY_APP_CODE
} from '@shared-sm';
import {
  DformPaginationPlusComponent,
  TYPE_BTN_FOOTER,
} from '@shared-sm';
import {
  PageEvent
} from '@angular/material/paginator';
import {
  MatTableDataSource
} from '@angular/material/table';
import {
  ROLE_MAKER,
  ROLE_APPROVER
} from './../constants';
import {
  code, description,
  name, status, className, startTime, expression
} from '../data-form/create-schedule-job';
import {FormBuilder, FormControl} from "@angular/forms";

import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {UpdateCategoryComponent} from "../../category-public/update-category/update-category.component";
import {ComponentDialogAbstract} from "../../../shared/abstract/component-dialog.abstract";
import {CronGenComponent, CronOptions} from "ngx-cron-editor";

@Component({
  selector: 'app-create-category',
  templateUrl: './create-schedule-job.component.html',
  styleUrls: ['./create-schedule-job.component.scss']
})
export class CreateScheduleJobComponent extends ComponentDialogAbstract {


  public cronExpression = '0 10 * * MON-FRI'; //"0 0 1/1 * *";

  @ViewChild('cronEditorDemo', {static: true})
  cronEditorDemo: CronGenComponent;

  public txtCron: string;
  public isCronDisabled = false;

  public cronOptions: CronOptions = {
    formInputClass: 'form-control cron-editor-input',
    formSelectClass: 'form-control cron-editor-select-custom',
    formRadioClass: 'cron-editor-radio',
    formCheckboxClass: 'cron-editor-checkbox',
    defaultTime: '00:00:00',
    hideMinutesTab: false,
    hideHourlyTab: false,
    hideDailyTab: false,
    hideWeeklyTab: false,
    hideMonthlyTab: true,
    hideYearlyTab: true,
    hideAdvancedTab: false,
    hideSpecificWeekDayTab: false,
    hideSpecificMonthWeekTab: false,
    use24HourTime: true,
    hideSeconds: false,
    cronFlavor: 'quartz',
  };

  $code = code();
  $name = name();
  $startTime = startTime();
  // $status = status();
  $description = description();
  $className = className();
  $expression = expression();

  constructor(protected injector: Injector,
              public formBuilder: FormBuilder,
              public dialogRef: MatDialogRef<CreateScheduleJobComponent>,
              @Inject(MAT_DIALOG_DATA) private dataDialog: any) {
    super(injector);

  }

  protected initData(): void {
    this.form = this.itemControl.toFormGroup([
      this.$code,
      this.$name,
      this.$startTime,
      this.$description,
      this.$className,
      this.$expression
    ]);

    this.initReactiveModel();

    this.cronForm = new FormControl(this.cronExpression);
    this.show = true;
  }

  public cronForm;

  public show = false;

  // public ngOnInit() {
  //   this.initReactiveModel();
  //
  //   this.cronForm = new FormControl(this.cronExpression);
  //   this.show = true;
  // }

  // public refresh() {
  //   this.cronForm.patchValue('0 12 * * MON-FRI');
  //
  //   this.cronExpression = this.txtCron;
  //   this.cronForm.patchValue(this.cronExpression);
  // }
  //
  // public reload() {
  //   this.cronExpression = this.txtCron;
  //   this.cronForm.patchValue(this.cronExpression);
  // }

  public initReactiveModel() {
    this.cronForm = this.formBuilder.group({
      cronExpression: new FormControl('0 0/1 * 1/1 * ? *'),
    });
    this.form.get('expression').setValue(this.cronForm.get('cronExpression').value)
  }

  closeDialog() {
    if (this.dialogRef.close) {
      this.dialogRef.close(null);
    }
  }

  saveData() {
    if (this.form.valid) {
      const formData = this.form.getRawValue();
      const startTime = formData.startTime.toISOString()
      const body = {
        schedule: {
          startTime: startTime,
          expression: formData.expression
        },
        ...formData,
        appCode: localStorage.getItem(CURRENCY_APP_CODE)
      }
      this.dialogRef.close(body)
    }
  }

  cronChange($event: any) {
    this.form.get('expression').setValue($event)
  }


}
