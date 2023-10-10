import { AppSettings } from './../../shared/settings';
import { SettingsService } from './../../shared/services/settings.service';
import { Directionality } from '@angular/cdk/bidi';
import { BreakpointObserver } from '@angular/cdk/layout';
import { OverlayContainer } from '@angular/cdk/overlay';
import { DOCUMENT } from '@angular/common';
import {
  Component,
  ElementRef, HostBinding, Inject, OnDestroy, OnInit,
  Optional, ViewChild,
  ViewEncapsulation
} from '@angular/core';
import { MatSidenav, MatSidenavContent } from '@angular/material/sidenav';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { filter } from 'rxjs/operators';
export const MOBILE_MEDIAQUERY = 'screen and (max-width: 599px)';
export const TABLET_MEDIAQUERY = 'screen and (min-width: 600px) and (max-width: 959px)';
export const MONITOR_MEDIAQUERY = 'screen and (min-width: 960px)';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class LayoutComponent implements OnInit, OnDestroy {
  @ViewChild('sidenav', { static: true }) sidenav: MatSidenav;
  @ViewChild('content', { static: true }) content: MatSidenavContent;
  routerSub: Subscription;
  options;
  isPortal;

  private layoutChangesSubscription: Subscription;

  private isMobileScreen = false;
  get isOver(): boolean {
    return this.isMobileScreen;
  }

  contentWidthFix = true;
  @HostBinding('class.dform-content-width-fix') get isContentWidthFix() {
    return (
      this.contentWidthFix &&
      this.options.navPos === 'side' &&
      this.options.sidenavOpened &&
      !this.isOver
    );
  }

  private collapsedWidthFix = true;
  @HostBinding('class.dform-sidenav-collapsed-fix') get isCollapsedWidthFix() {
    return (
      this.collapsedWidthFix &&
      (this.options.navPos === 'top' || (this.options.sidenavOpened && this.isOver))
    );
  }

  constructor(
    private router: Router,
    private breakpointObserver: BreakpointObserver,
    private overlay: OverlayContainer,
    private element: ElementRef,
    private settings: SettingsService,
    @Optional() @Inject(DOCUMENT) private document: Document,
  ) {
    this.options = this.settings.getOptions();
    this.layoutChangesSubscription = this.breakpointObserver
      .observe([MOBILE_MEDIAQUERY, TABLET_MEDIAQUERY, MONITOR_MEDIAQUERY])
      .subscribe(state => {
        // this.isMobileScreen = state.breakpoints[MOBILE_MEDIAQUERY] || state.breakpoints[TABLET_MEDIAQUERY];
        // this.options.sidenavCollapsed = state.breakpoints[TABLET_MEDIAQUERY];
        // this.contentWidthFix = state.breakpoints[MONITOR_MEDIAQUERY];
        // SidenavOpened must be reset true when layout changes
        this.isMobileScreen = false;
        this.options.sidenavOpened = this.isOver ? false : true;
        this.resetCollapsedState();
      });


    this.routerSub = this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe((evt: NavigationEnd) => {
        // this.content.scrollTo({ top: 0 });
        this.checkShowMenu(evt.urlAfterRedirects !== '/dashboard');
      });
    this.isPortal = localStorage.getItem('token') ? true : false;
    console.log(this.isPortal)
  }
  ngOnInit() {
    setTimeout(() => (this.contentWidthFix = this.collapsedWidthFix = false));
  }

  ngOnDestroy() {
    this.layoutChangesSubscription.unsubscribe();
  }

  toggleCollapsed() {
    this.options.sidenavCollapsed = !this.options.sidenavCollapsed;
    this.resetCollapsedState();
  }

  resetCollapsedState(timer = 400) {
    // TODO: Trigger when transition end
    setTimeout(() => {
      this.settings.setNavState('collapsed', this.options.sidenavCollapsed);
    }, timer);
  }

  sidenavCloseStart() {
    this.contentWidthFix = false;
  }

  sidenavOpenedChange(isOpened: boolean) {
    this.options.sidenavOpened = isOpened;
    this.settings.setNavState('opened', isOpened);

    this.collapsedWidthFix = !this.isOver;
    this.resetCollapsedState();
  }

  /** Demo purposes only */
  receiveOptions(options: AppSettings): void {
    this.options = options;
    this.toggleDarkTheme(options);
    this.toggleDirection(options);
  }

  toggleDarkTheme(options: AppSettings) {
    if (options.theme === 'dark') {
      this.element.nativeElement.classList.add('theme-dark');
      this.overlay.getContainerElement().classList.add('theme-dark');
    } else {
      this.element.nativeElement.classList.remove('theme-dark');
      this.overlay.getContainerElement().classList.remove('theme-dark');
    }
  }

  toggleDirection(options: AppSettings) {
    // this.dir.value = options.dir;
    // this.document.body.dir = this.dir.value;
  }

  private checkShowMenu(showMenu) {
    if (showMenu) {
      this.options.sidenavOpened = true;
      this.options.navPos = 'side';
      this.options.menuMode = 1;
    } else {
      this.options.sidenavOpened = false;
      this.options.navPos = 'top';
      this.options.menuMode = 0;
    }
  }
}
