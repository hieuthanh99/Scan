import {
    SharedSMModule
} from '@shared-sm';
import {
    CategoryScopeRoutingModule
} from './category-scope-routing.module';
import {
    NgModule
} from '@angular/core';
import {
    CommonModule
} from '@angular/common';
import {
    MatTableModule
} from '@angular/material/table';
import {MatMenuModule} from '@angular/material/menu';
import { MatExpansionModule } from '@angular/material/expansion';
import {
    FlexLayoutModule
} from '@angular/flex-layout';
import {
    MatIconModule
} from '@angular/material/icon';
import {
    CategoryScopeComponent
} from './category-scope/category-scope.component';
import {
    CategoryScopeDetailComponent
} from './category-scope-detail/category-scope-detail.component';
@NgModule({
    declarations: [
        CategoryScopeDetailComponent,
        CategoryScopeComponent,
    ],
    imports: [
        MatExpansionModule,
        CommonModule,
        MatTableModule,
        FlexLayoutModule,
        MatIconModule,
        CategoryScopeRoutingModule,
        SharedSMModule,
        MatMenuModule,
    ]
})
export class CatgoryScopeModule {}