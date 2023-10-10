import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RedisListComponent } from './redis-list/redis-list.component';

const routes: Routes = [
  {
    path: 'list',
    component: RedisListComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RedisManagementRoutingModule {}
