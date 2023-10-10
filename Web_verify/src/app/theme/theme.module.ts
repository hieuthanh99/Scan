import { MatRippleModule } from '@angular/material/core';
import { DformDialogService } from './../shared/dform-dialogs/dialog.service';
import { MatDialogModule } from '@angular/material/dialog';
import { ActivityIndicatorModule } from './../shared/activity-indicator/activityIndicator.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { RouterModule } from '@angular/router';
import { AccordionModule } from './accordion/accordion.module';
import { LayoutComponent } from './layout/layout.component';
import { HeaderComponent } from './header/header.component';
import { SearchHeaderComponent } from './header/widgets/search-header.component';
import { UserComponent } from './header/widgets/user.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { UserPanelComponent } from './sidebar/user-panel.component';
import { SidemenuComponent } from './sidemenu/sidemenu.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';
@NgModule({
  declarations: [
    LayoutComponent,
    SidebarComponent,
    UserPanelComponent,
    SidemenuComponent,
    HeaderComponent,
    UserComponent,
    SearchHeaderComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatMenuModule,
    MatIconModule,
    MatListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatButtonModule,
    MatButtonToggleModule,
    FlexLayoutModule,
    AccordionModule,
    ActivityIndicatorModule,
    MatDialogModule,
    NgSelectModule,
    FormsModule,
    MatRippleModule
  ],
  providers: [DformDialogService]
})
export class ThemeModule { }
