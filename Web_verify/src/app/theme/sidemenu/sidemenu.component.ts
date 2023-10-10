import { Component, Input, ViewEncapsulation } from '@angular/core';
import { LocalStoreManagerService } from '@shared-sm';

@Component({
  selector: 'app-sidemenu',
  templateUrl: './sidemenu.component.html',
  styleUrls: ['./sidemenu.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class SidemenuComponent {
  @Input() ripple = true;

  basePath = '/cms_prortal'
  menus = [
    {
      "route": "/",
      "name": "Trang chủ",
      "type": "link",
      "icon": "mbb-icon ic-bank",
      "children": []
    },
    {
      "route": "",
      "name": "Cấu hình",
      "type": "sub",
      "icon": "mbb-icon ic-maintenance",
      "children": [
        {
          "route": `${this.basePath}/category-public/list`,
          "name": "Danh mục chung",
          "type": "link",
          "icon": null,
          "children": []
        },
        {
          "route": `${this.basePath}/schedule-job/list`,
          "name": "Quản lý tiến trình",
          "type": "link",
          "icon": null,
          "children": []
        },
        {
          "route": `${this.basePath}/redis-management/list`,
          "name": "Quản lý Redis",
          "type": "link",
          "icon": null,
          "children": []
        },
        {
          "route": `${this.basePath}/mail-template/list`,
          "name": "Quản lý Mail Template",
          "type": "link",
          "icon": null,
          "children": []
        }
      ]
    },
    {
      "route": "",
      "name": "Danh mục",
      "type": "sub",
      "icon": "mbb-icon ic-maintenance",
      "children": [
        {
          "route": "/cms_prortal/category-system/list",
          "name": "Danh mục hệ thống",
          "type": "link",
          "icon": null,
          "children": []
        },
        {
          "route": "/cms_prortal/category-scope/list",
          "name": "Danh mục thao tác",
          "type": "link",
          "icon": null,
          "children": []
        },
        {
          "route": "/cms_prortal/category-user/list",
          "name": "Danh mục người dùng",
          "type": "link",
          "icon": null,
          "children": []
        },
        {
          "route": "/cms_prortal/category-role/list",
          "name": "Danh mục vai trò",
          "type": "link",
          "icon": null,
          "children": []
        },
        {
          "route": "/cms_prortal/category-resource/list",
          "name": "Danh mục chức năng",
          "type": "link",
          "icon": null,
          "children": []
        },
      ]
    }
  ]
  winData;
  route;
  isLoad = false;
  constructor(
    private localStore: LocalStoreManagerService) {
  }

  // Delete empty values and rebuild route
  buildRoute(routes: string[]) {
    let route = '';
    routes.forEach(item => {
      if (item && item.trim()) {
        route += '/' + item.replace(/^\/+|\/+$/g, '');
      }
    });
    return route;
  }


  loadUrl(route) {
    // this.isLoad = true;
    // this.route = route;
    // this.winData = window.open(this.url, "_blank");
    // window.addEventListener('message', this.load, false);
  }

  load = (event) => {
    // if (this.isLoad && event.data && (typeof event.data === 'string' || event.data instanceof String) && event.data === 'ping') {
    //   const token = this.localStore.getData(LocalStoreEnum.Token)
    //   this.winData.postMessage({ token, refUrl: this.route }, this.url);
    //   this.isLoad = false;
    // }
  }

}
