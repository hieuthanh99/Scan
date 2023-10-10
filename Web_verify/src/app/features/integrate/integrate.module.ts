import {MatButtonModule} from '@angular/material/button';
import {CommonModule, DatePipe} from '@angular/common';
import {NgModule} from '@angular/core';
import {FlexLayoutModule} from '@angular/flex-layout';
import {MatIconModule} from '@angular/material/icon';
import {SharedSMModule} from '@shared-sm';
import {IntegrateForBpmComponent} from "./integrate-for-bpm/integrate-for-bpm.component";
import {BpmCompleteProfileComponent} from "./integrate-for-bpm/bpm-complete-profile/bpm-complete-profile.component";
import {BpmProfileCompletedComponent} from "./integrate-for-bpm/bpm-profile-completed/bpm-profile-completed.component";
import {MatTabsModule} from "@angular/material/tabs";
import {IntegrateRoutingModule} from "./integrate-routing.module";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatCheckboxModule} from "@angular/material/checkbox";
import {MatMenuModule} from "@angular/material/menu";
import {ImgSrcSecurityPipe} from "../../shared/pipe/img-src-security-pipe";

@NgModule({
  declarations: [
    IntegrateForBpmComponent,
    BpmCompleteProfileComponent,
    BpmProfileCompletedComponent,
    ImgSrcSecurityPipe
  ],
  imports: [
    CommonModule,
    FlexLayoutModule,
    MatIconModule,
    MatButtonModule,
    MatTabsModule,
    SharedSMModule,
    MatFormFieldModule,
    MatInputModule,
    MatCheckboxModule,
    MatMenuModule,
    IntegrateRoutingModule,
  ],
  providers: [
    DatePipe,
  ]
})
export class IntegrateModule {
}
