import { NgModule } from '@angular/core';
import {
    PreloadAllModules,
    RouterModule,
    Routes
} from '@angular/router';
import { HomePageComponent } from './features/home-page/home-page/home-page.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { AuthKeycloakGuard } from './shared/guards/auth-keycloak.guard';
import { AuthGuard } from './shared/guards/auth.guard';
import { LayoutComponent } from './theme/layout/layout.component';
import {AuthIframeGuard} from "./shared/guards/auth.iframe.guard";

export const checkJwt = () => {
    const jwt = localStorage.getItem('token')
    return jwt;
}

const routes: Routes = [
    {
        path: '',
        redirectTo: 'cms_prortal/home-page',
        pathMatch: 'full'
    },
    {
        path: 'cms_prortal/home-page',
        component: HomePageComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
    },
    {
        path: 'cms_prortal',
        redirectTo: 'cms_prortal/category-public',
        pathMatch: 'full'
    },
    {
        path: 'cms_prortal/category-public',
        component: LayoutComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
        loadChildren: () => import('./features/category-public/category-public.module').then(m => m.CategoryPublicModule),
    },
    {
        path: 'cms_prortal/redis-management',
        component: LayoutComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
        loadChildren: () => import('./features/redis-management/redis-management.module').then(m => m.RedisManagementModule),
    },
    {
        path: 'cms_prortal/category-scope',
        component: LayoutComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
        loadChildren: () => import('./features/category-scope/category-scope.module').then(m => m.CatgoryScopeModule),
    },
    {
        path: 'cms_prortal/category-system',
        component: LayoutComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
        loadChildren: () => import('./features/category-system/category-system.module').then(m => m.CategorySystemModule),

    },
    {
        path: 'cms_prortal/category-user',
        component: LayoutComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
        loadChildren: () => import('./features/category-user/category-user.module').then(m => m.CategoryUserModule),
    },
    {

        path: 'cms_prortal/category-role',
        component: LayoutComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
        loadChildren: () => import('./features/category-role/category-role.module').then(m => m.CategoryRoleModule),
    },
    {
        path: 'cms_prortal/category-resource',
        component: LayoutComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
        loadChildren: () => import('./features/category-resource/category-resource.module').then(m => m.CategoryResourceModule),
    },
    {
        path: 'cms_prortal/schedule-job',
        component: LayoutComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
        loadChildren: () => import('./features/schedule-job/schedule-job.module').then(m => m.ScheduleJobModule),
    },
    {
        path: 'cms_prortal/mail-template',
        component: LayoutComponent,
        canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
        loadChildren: () => import('./features/mail-template/mail-template.module').then(m => m.MailTemplateModule),
    },
    {
        path: 'integrate',
        canActivate: [AuthIframeGuard],
        loadChildren: ()=>import('./features/integrate/integrate.module').then(m=>m.IntegrateModule),
    },
    {
        path: '**',
        component: NotFoundComponent
    },
    // {
    //     path: 'home-page',
    //     component: SurveyComponent,
    //     canActivate: [checkJwt() ? AuthGuard : AuthKeycloakGuard],
    //     children: [
    //         { path: '', component: SurveyComponent },
    //     ]
    // },
];
@NgModule({
    imports: [RouterModule.forRoot(routes, {
        preloadingStrategy: PreloadAllModules
    })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
