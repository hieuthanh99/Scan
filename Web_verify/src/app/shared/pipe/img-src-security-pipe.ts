import {Pipe, PipeTransform} from '@angular/core';
import {DomSanitizer, SafeUrl} from '@angular/platform-browser';
import {Observable} from "rxjs";
import {map} from "rxjs/operators";
import {LocalStoreManagerService} from '@shared-sm';
import {HttpClient, HttpHeaders} from '@angular/common/http';

@Pipe({
  name: 'imgSrcSecurity'
})
export class ImgSrcSecurityPipe implements PipeTransform {

  constructor(private http: HttpClient, private sanitizer: DomSanitizer, private localStore: LocalStoreManagerService) {
  }

  transform(url): Observable<SafeUrl> {
    return this.http
      .get(url, {
        headers: new HttpHeaders().set('Authorization', `Bearer ${this.localStore.getData('token')}`),
        responseType: 'blob'
      })
      .pipe(map(val => this.sanitizer.bypassSecurityTrustUrl(URL.createObjectURL(val))));
  }

}
