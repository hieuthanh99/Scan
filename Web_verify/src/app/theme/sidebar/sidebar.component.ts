import { Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogManagementService, ListDashBoard } from '@shared-sm';
import { SettingsService } from './../../shared/services/settings.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class SidebarComponent implements OnInit {
  @Output() toggleSidenav = new EventEmitter<void>();
  @Output() toggleCollapsed = new EventEmitter<void>();
  @Input() showToggle = true;
  options;
  constructor(
    public dialog: MatDialog,
    private dialogManagementService: DialogManagementService,
    private settings: SettingsService,
  ) {
    this.options = this.settings.getOptions()
  }

  ngOnInit() {
  }
}