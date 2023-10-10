import {
    SharedSMModule
} from '@shared-sm';
import {
    CategoryResourceRoutingModule
} from './category-resource-routing.module';
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
    CategoryResourceComponent
} from './category-resource/category-resource.component';
import {
    CategoryResourceEditComponent
} from './category-resource-edit/category-resource-edit.component';
import {
    CategoryResourceCreateComponent
} from './category-resource-create/category-resource-create.component';
import { CategoryResourceDetailComponent } from './category-resource-detail/category-resource-detail.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTreeModule } from '@angular/material/tree';
import {MatProgressBarModule} from '@angular/material/progress-bar';

@NgModule({
    declarations: [
        CategoryResourceComponent,
        CategoryResourceEditComponent,
        CategoryResourceCreateComponent,
        CategoryResourceDetailComponent,
    ],
    imports: [
        CommonModule,
        MatTableModule,
        FlexLayoutModule,
        MatIconModule,
        CategoryResourceRoutingModule,
        SharedSMModule,
        MatExpansionModule,
        MatTreeModule,
        MatProgressBarModule,
    ]
})
export class CategoryResourceModule { }