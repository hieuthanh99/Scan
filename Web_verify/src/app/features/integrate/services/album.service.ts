import {Injectable} from '@angular/core';
import {HttpClientService, HttpOptions} from '@shared-sm';
import {Observable} from "rxjs";
import {environment} from "@env/environment";
import {API_PATH} from "../constants";
import {PdfDto} from "../dtos/pdf-dto";
import {HttpHeaders, HttpParams, HttpResponse} from "@angular/common/http";

@Injectable({
  providedIn: 'root',
})
export class AlbumService {
  constructor(private httpClient: HttpClientService) {
  }

  getAllActiveAlbum(): Observable<any> {
    const options: HttpOptions = {
      url: environment.smartScanApi,
      path: API_PATH.ALBUM.GET_ALL_ACTIVE,
      params : new HttpParams().set("size", 200)
    };
    return this.httpClient.get<any>(options);
  }

  findPdf(text: string): Observable<PdfDto> {
    const options: HttpOptions = {
      url: environment.smartScanApi,
      path: API_PATH.ALBUM.FIND_PDF,
      params: new HttpParams().set("text", text).set("pageNumber", 0).set("pageSize", 9999)
    };
    return this.httpClient.get<PdfDto>(options);
  }

  viewImageS3(path : string): Observable<HttpResponse<Blob>> {
    const options: HttpOptions = {
      url: environment.smartScanApi,
      path: API_PATH.ALBUM.VIEW_IMAGE_S3,
      params: new HttpParams().set("path", path),
      responseType: "blob",
    };
    return this.httpClient.get<HttpResponse<Blob>>(options);
  }
}
