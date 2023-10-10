import { DialogManagementService } from './../services/dialog-management.service';
import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DformConfirmComponent } from './dform-confirm/dform-confirm.component';
import { DialogConfirmComponent } from './dialog-confirm/dialog-confirm.component';
import { DialogErrorComponent } from './dialog-error/dialog-error.component';
import { DialogSuccessComponent } from './dialog-success/dialog-success.component';
import { IConfirmModel, IErrorModel } from './dialog.model';

@Injectable()
export class DformDialogService {
  constructor(
    private dialog: MatDialog,
    private dialogManagementService: DialogManagementService
  ) { }

  error(data: IErrorModel, callback?: (result: boolean) => void) {
    const dialogRef = this.dialog.open(DialogErrorComponent, { data, minWidth: '450px' });
    if (callback) {
      dialogRef.afterClosed().subscribe(callback);
    }
    this.dialogManagementService.addDialogRef(dialogRef);
  }

  success(data: IErrorModel, callback?: (result: boolean) => void) {
    const dialogRef = this.dialog.open(DialogSuccessComponent, { data, minWidth: '450px' });
    if (callback) {
      dialogRef.afterClosed().subscribe(callback);
    }
    this.dialogManagementService.addDialogRef(dialogRef);
  }

  confirm(data: IConfirmModel, callback: (result: boolean) => void) {
    const dialogRef = this.dialog.open(DialogConfirmComponent, { data, minWidth: '450px' });
    dialogRef.afterClosed().subscribe(callback);
    this.dialogManagementService.addDialogRef(dialogRef);
  }

  componentDialog(component: any, data: any, callback: any) {
    const dialogRef = this.dialog.open(component, data);
    dialogRef.afterClosed().subscribe(callback);
    this.dialogManagementService.addDialogRef(dialogRef);
  }

  dformconfirm(data: IConfirmModel, callback: (result: boolean) => void) {
    const dialogRef = this.dialog.open(DformConfirmComponent,
      { data, minWidth: '450px' });
    dialogRef.afterClosed().subscribe(callback);
    this.dialogManagementService.addDialogRef(dialogRef);
  }

  closeAll() {
    this.dialogManagementService.closeAll();
  }
}
