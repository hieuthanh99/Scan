import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { SharedSMModule } from '@shared-sm';
import { CategoryPublicRoutingModule } from '../category-public/category-public-routing.module';
import { RedisListComponent } from './redis-list/redis-list.component';
import { RedisManagementRoutingModule } from './redis-management-routing.module';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
    declarations: [RedisListComponent],
    imports: [
        CommonModule,
        RedisManagementRoutingModule,
        MatTableModule,
        FlexLayoutModule,
        MatIconModule,
        CategoryPublicRoutingModule,
        SharedSMModule,
        MatButtonModule,
        MatTooltipModule,
    ],
})
export class RedisManagementModule {}
