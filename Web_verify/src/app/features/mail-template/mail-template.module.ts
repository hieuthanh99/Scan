import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MailTemplateRoutingModule } from './mail-template-routing.module';
import { MailTemplateListComponent } from './mail-template-list/mail-template-list.component';
import { MatTableModule } from '@angular/material/table';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatIconModule } from '@angular/material/icon';
import { SharedSMModule } from '@shared-sm';
import { MatMenuModule } from '@angular/material/menu';
import { CreateMailTemplateComponent } from './create-mail-template/create-mail-template.component';
import { UpdateMailTemplateComponent } from './update-mail-template/update-mail-template.component';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
  declarations: [
    MailTemplateListComponent,
    CreateMailTemplateComponent,
    UpdateMailTemplateComponent
  ],
  imports: [
    CommonModule,
    MailTemplateRoutingModule,
    MatTableModule,
    FlexLayoutModule,
    MatIconModule,
    SharedSMModule,
    MatMenuModule,
    MatExpansionModule,
    MatTooltipModule,
  ]
})
export class MailTemplateModule { }
