import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

@Component({
  selector: 'mbb-dform-confirm',
  templateUrl: './dform-confirm.component.html',
  styleUrls: ['./dform-confirm.component.scss']
})
export class DformConfirmComponent{
  confirm!: string;
  isRequired = false;
  characterNumberandTex = /[^A-Za-z0-9 ]/g;
  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<DformConfirmComponent>,
  ) {
    this.dialogRef.disableClose = true;
  }

  onCancelConfirm() {
    if (!this.confirm && this.data?.description && this.data?.closeBtn != 'btn.cancel'){
      this.isRequired = true;
    } else {
      if (this.dialogRef.close) {
        this.dialogRef.close({ status: 1, data: this.confirm });
      }
    }
  }

  onUpdateConfirm() {
    if (!this.confirm && this.data?.description && this.data?.acceptBtn !== 'btn.accept'){
      this.isRequired = true;
    } else {
      if (this.dialogRef.close) {
        this.dialogRef.close({ status: 2, data: this.confirm });
      }
    }
  }

  closeDialog() {
    if (this.dialogRef.close) { this.dialogRef.close({ status: 0 }); }
  }

  ngModelChange() {
    this.confirm = this.confirm.trim();
  }
}
