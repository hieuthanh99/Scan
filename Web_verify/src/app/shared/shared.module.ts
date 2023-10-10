import { FlexLayoutModule } from '@angular/flex-layout';
import { TranslateModule } from '@ngx-translate/core';
import { DformPaginationPlusModule } from './dform-pagination-plus/dform-pagination-plus.module';
import { DformDialogModule } from './dform-dialogs/dialog.module';
import { ActivityIndicatorModule } from './activity-indicator/activityIndicator.module';
import { FormGroupDformModule } from './form-group/form-group.module';
import { TemplateModule } from './template/template.module';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';


@NgModule({
  imports: [
    FormGroupDformModule,
    TemplateModule,
    ActivityIndicatorModule,
    DformDialogModule,
    DformPaginationPlusModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    FlexLayoutModule
  ],
  exports: [
    FormGroupDformModule,
    TemplateModule,
    ActivityIndicatorModule,
    DformDialogModule,
    DformPaginationPlusModule,
    TranslateModule,
    FormsModule,
    RouterModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    ToastrModule
    
  ]
})
export class SharedSMModule { }
