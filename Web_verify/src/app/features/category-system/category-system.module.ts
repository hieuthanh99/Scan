import {
    SharedSMModule
} from '@shared-sm';
import {
    CategorySystemRoutingModule
} from './category-system-routing.module';
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
    CategorySystemComponent
} from './category-system/category-system.component';
import {
    CategorySystemEditComponent
} from './category-system-edit/category-system-edit.component';
import {
    CategorySystemCreateComponent
} from './category-system-create/category-system-create.component';
import { MatExpansionModule } from '@angular/material/expansion';
@NgModule({
    declarations: [
        CategorySystemComponent,
        CategorySystemEditComponent,
        CategorySystemCreateComponent,
    ],
    imports: [
        CommonModule,
        MatTableModule,
        FlexLayoutModule,
        MatIconModule,
        CategorySystemRoutingModule,
        SharedSMModule,
        MatExpansionModule
    ]
})
export class CategorySystemModule { }