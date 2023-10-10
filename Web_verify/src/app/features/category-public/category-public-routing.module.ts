import {
    NgModule
} from '@angular/core';
import {
    RouterModule,
    Routes
} from '@angular/router';
import {
    CategoryPublicComponent
} from './category-public/category-public.component';
import {
    CreateCategoryComponent
} from './create-category/create-category.component';
import {
    UpdateCategoryComponent
} from './update-category/update-category.component';
const routes: Routes = [
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    }, {
        path: 'list',
        component: CategoryPublicComponent
    }, {
        path: 'create',
        component: CreateCategoryComponent
    }, {
        path: 'update',
        component: UpdateCategoryComponent
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoryPublicRoutingModule { }
