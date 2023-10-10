import {
    NgModule
} from '@angular/core';
import {
    RouterModule,
    Routes
} from '@angular/router';
import {
    CategoryRoleComponent
} from './category-role/category-role.component';
const routes: Routes = [{
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
},
{
    path: 'list',
    component: CategoryRoleComponent
}];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoryRoleRoutingModule { }