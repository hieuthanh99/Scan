import {MatButtonModule} from '@angular/material/button';
import {
  CommonModule
} from '@angular/common';
import {
  NgModule
} from '@angular/core';
import {
  FlexLayoutModule
} from '@angular/flex-layout';
import {
  MatIconModule
} from '@angular/material/icon';
import {
  MatTableModule
} from '@angular/material/table';
import {
  SharedSMModule
} from '@shared-sm';
import {
  ScheduleJobRoutingModule
} from './schedule-job-routing.module';
import {
  ListScheduleJobComponent
} from './list-schedule-job/list-schedule-job.component';
import {
  CreateScheduleJobComponent
} from './create-schedule-job/create-schedule-job.component';
import {
  UpdateScheduleJobComponent
} from './update-schedule-job/update-schedule-job.component';
import {CronEditorModule} from "ngx-cron-editor";
import {ListHistoryExecuteJobComponent} from "./list-history-execute-job/list-history-execute-job.component";

@NgModule({
  declarations: [
    ListScheduleJobComponent,
    CreateScheduleJobComponent,
    UpdateScheduleJobComponent,
    ListHistoryExecuteJobComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    FlexLayoutModule,
    MatIconModule,
    ScheduleJobRoutingModule,
    SharedSMModule,
    CronEditorModule,
    MatButtonModule
  ]
})
export class ScheduleJobModule {
}
