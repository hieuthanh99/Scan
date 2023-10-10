import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {IntegrateForBpmComponent} from "./integrate-for-bpm/integrate-for-bpm.component";
import {AuthIframeGuard} from "../../shared/guards/auth.iframe.guard";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'bpm',
    pathMatch: 'full'
  }, {
    path: 'bpm',
    canActivate: [AuthIframeGuard],
    component: IntegrateForBpmComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IntegrateRoutingModule {
}
