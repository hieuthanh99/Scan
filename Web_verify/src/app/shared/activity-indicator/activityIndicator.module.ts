import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ActivityIndicatorComponent } from './activity-indicator-singleton.component';
@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    ActivityIndicatorComponent
  ],
  exports: [
    ActivityIndicatorComponent
  ],
  providers: [
  ]
})
export class ActivityIndicatorModule {}
