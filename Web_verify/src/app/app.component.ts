import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '@env/environment';

declare const require: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit, OnDestroy {

  ngVersion = require('../../package.json').dependencies['@angular/core'];
  constructor(private router: Router) {
    this.addStyle();
  }

  ngOnInit(): void {
    const url = location.href?.replace(location.origin, '');
    this.router.navigateByUrl(url);
    window.addEventListener('message', (event) => {
      const urlNavigation = location.href?.replace(location.origin, '');
      if (event && event.data == environment.key) {
        this.router.navigateByUrl(urlNavigation);
      }
    });
  }

  ngOnDestroy(): void {
    this.removeStyle();
  }

  addStyle() {
    var cssId = 'myCss';  // you could encode the css path itself to generate id..
    if (!document.getElementById(cssId)) {
      var head = document.getElementsByTagName('head')[0];
      var link = document.createElement('link');
      link.id = cssId;
      link.rel = 'stylesheet';
      link.type = 'text/css';
      link.href = 'http://localhost:4200/styles.css';
      link.media = 'all';
      head.appendChild(link);
    }
  }

  removeStyle(){
    var cssId = 'myCss';  // you could encode the css path itself to generate id..
    document.getElementById(cssId)?.remove();
  }
}
