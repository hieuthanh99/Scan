import {
    NgModule
} from '@angular/core';
import {
    RouterModule,
    Routes
} from '@angular/router';
import {
    CategoryScopeComponent
} from './category-scope/category-scope.component';
const routes: Routes = [{
    path: '',
    redirectTo: 'list',
    pathMatch: 'full'
},
{
    path: 'list',
    component: CategoryScopeComponent
}];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class CategoryScopeRoutingModule { }