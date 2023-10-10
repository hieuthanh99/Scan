import {Component, Injector, ViewEncapsulation} from '@angular/core';
import {MatTabChangeEvent} from "@angular/material/tabs";
import {AlbumService} from "../services/album.service";
import {ComponentAbstract} from "@shared-sm";
import {finalize, takeUntil} from "rxjs";

@Component({
  selector: 'app-integrate-for-bpm',
  templateUrl: './integrate-for-bpm.component.html',
  styleUrls: ['./integrate-for-bpm.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class IntegrateForBpmComponent extends ComponentAbstract {
  tabIndex: number = 0;
  albums: any[];

  constructor(protected injector: Injector, private albumService: AlbumService) {
    super(injector);
  }

  protected componentInit(): void {
    console.log('AWESOME!!!');
  }

  tabChange(tabIndex: number) {
    this.tabIndex = tabIndex;
  }

  onTabChanged(changeEvent: MatTabChangeEvent) {
    if (changeEvent.index === 1) {
      this.loadAllActiveAlbum();
    }
  }

  loadAllActiveAlbum() {
    this.indicator.showActivityIndicator();
    try {
      this.albumService.getAllActiveAlbum().pipe(
        takeUntil(this.ngUnsubscribe),
        finalize(() => this.indicator.hideActivityIndicator())
      ).subscribe((res: any) => {
        if (res) {
          console.log(res);
        }
      });
    } catch (e) {
      debugger
    }
  }
}
