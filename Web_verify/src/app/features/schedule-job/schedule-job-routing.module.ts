import {
    NgModule
} from '@angular/core';
import {
    RouterModule,
    Routes
} from '@angular/router';
import {
    ListScheduleJobComponent
} from './list-schedule-job/list-schedule-job.component';
import {
    CreateScheduleJobComponent
} from './create-schedule-job/create-schedule-job.component';
import {
    UpdateScheduleJobComponent
} from './update-schedule-job/update-schedule-job.component';
const routes: Routes = [
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    }, {
        path: 'list',
        component: ListScheduleJobComponent
    }, {
        path: 'create',
        component: CreateScheduleJobComponent
    }, {
        path: 'update',
        component: UpdateScheduleJobComponent
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ScheduleJobRoutingModule { }
