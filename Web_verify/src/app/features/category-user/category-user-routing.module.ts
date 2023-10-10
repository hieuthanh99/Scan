import {
    NgModule
} from '@angular/core';
import {
    RouterModule,
    Routes
} from '@angular/router';
import { CategoryUserEditComponent } from './category-user-edit/category-user-edit.component';
import {
    CategoryUserComponent
} from './category-user/category-user.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: 'list',
        pathMatch: 'full'
    },
    {
        path: 'list',
        component: CategoryUserComponent
    },
    {
        path: 'edit',
        component: CategoryUserEditComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoryUserRoutingModule { }