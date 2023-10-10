import { MatButtonModule } from '@angular/material/button';
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
    CategoryPublicRoutingModule
} from './category-public-routing.module';
import {
    CategoryPublicComponent
} from './category-public/category-public.component';
import {
    CreateCategoryComponent
} from './create-category/create-category.component';
import {
    UpdateCategoryComponent
} from './update-category/update-category.component';
import {CategoriesCommonComponent} from "./categories-common/categories-common.component";
@NgModule({
    declarations: [
        CategoryPublicComponent,
        CategoriesCommonComponent,
        CreateCategoryComponent,
        UpdateCategoryComponent,
    ],
    imports: [
        CommonModule,
        MatTableModule,
        FlexLayoutModule,
        MatIconModule,
        CategoryPublicRoutingModule,
        SharedSMModule,
        MatButtonModule,
        SharedSMModule
    ]
})
export class CategoryPublicModule { }
