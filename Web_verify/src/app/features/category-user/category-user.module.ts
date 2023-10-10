import { MatExpansionModule } from '@angular/material/expansion';
import {
    SharedSMModule
} from '@shared-sm';
import {
    CategoryUserRoutingModule
} from './category-user-routing.module';
import {
    NgModule
} from '@angular/core';
import {
    CommonModule
} from '@angular/common';
import {
    MatTableModule
} from '@angular/material/table';
import {
    FlexLayoutModule
} from '@angular/flex-layout';
import {
    MatIconModule
} from '@angular/material/icon';
import {
    CategoryUserComponent
} from './category-user/category-user.component';
import {
    CategoryUserEditComponent
} from './category-user-edit/category-user-edit.component';
import {
    CategoryUserCreateComponent
} from './category-user-create/category-user-create.component';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
@NgModule({
    declarations: [
        CategoryUserComponent,
        CategoryUserEditComponent,
        CategoryUserCreateComponent
    ],
    imports: [
        MatExpansionModule,
        CommonModule,
        MatTableModule,
        FlexLayoutModule,
        MatIconModule,
        MatTooltipModule,
        MatMenuModule,
        CategoryUserRoutingModule,
        SharedSMModule,
    ]
})
export class CategoryUserModule { }