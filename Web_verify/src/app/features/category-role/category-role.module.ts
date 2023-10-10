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
import { MatMenuModule } from '@angular/material/menu';
import {
    MatTableModule
} from '@angular/material/table';
import {
    SharedSMModule
} from '@shared-sm';
import {
    CategoryRoleDetailComponent
} from './category-role-detail/category-role-detail.component';
import {
    CategoryRoleComponent
} from './category-role/category-role.component';
import {
    RoleUserComponent
} from './role-user/role-user.component';
import {
    CategoryRoleRoutingModule
} from './category-role-routing.module';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatCheckboxModule } from '@angular/material/checkbox';
@NgModule({
    declarations: [
        CategoryRoleDetailComponent,
        CategoryRoleComponent,
        RoleUserComponent
    ],
    imports: [
        MatExpansionModule,
        CommonModule,
        MatTableModule,
        FlexLayoutModule,
        MatIconModule,
        MatTooltipModule,
        CategoryRoleRoutingModule,
        SharedSMModule,
        MatMenuModule,
        MatCheckboxModule
    ]
})
export class CategoryRoleModule { }