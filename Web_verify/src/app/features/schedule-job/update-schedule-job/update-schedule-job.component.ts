import {
    Component,
    Injector,
    ViewChild
} from '@angular/core';
import {
    ComponentAbstract
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
    key,
    name
} from '../data-form/update-schedule-job';
@Component({
    selector: 'app-update-schedule-job',
    templateUrl: './update-schedule-job.component.html',
    styleUrls: ['./update-schedule-job.component.scss']
})
export class UpdateScheduleJobComponent extends ComponentAbstract {
    $key = key();
    $name = name()
    constructor(protected injector: Injector, ) {
        super(injector);
        this.initRole(ROLE_MAKER, ROLE_APPROVER);
    }
    componentInit(): void {
        this.form = this.itemControl.toFormGroup([this.$key, this.$name]);
    }
}
