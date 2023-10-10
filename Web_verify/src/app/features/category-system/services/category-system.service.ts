import { HttpParams } from '@angular/common/http';
import {
    Injectable
} from '@angular/core';
import {
    environment
} from '@env/environment';
import {
    HttpClientService,
    HttpOptions
} from '@shared-sm';
import {
    PATH
} from '../constants';
import ISystem from '../models/category-system.model';
@Injectable({
    providedIn: 'root',
})
export class CategorySystemService {
    constructor(private httpClient: HttpClientService) { }

    /**
     * Tìm kiếm ứng dụng
     * @param options 
     */
    search(appName: string, status: boolean, page: number, size: number) {
        var params: HttpParams = new HttpParams();
        if (appName != null && appName != '') params.append('appName', appName);
        if (status != null) params.append('status', status);
        params.append('page', page);
        params.append('size', size);
        const options: HttpOptions = {
            url: environment.hostApi,
            path: PATH.SEARCH,
            params: params
        }
        return this.httpClient.get(options);
    }

    /**
     * Tạo mới ứng dụng
     * @param body 
     */
    create(body: any) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: PATH.CREATE,
            body
        };
        return this.httpClient.post(options);
    }

    /**
     * Chỉnh sửa thông tin ứng dụng
     * @param body 
     */
    update(body: any) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: PATH.UPDATE,
            body
        };
        return this.httpClient.put(options);
    }

    /**
     * Chi tiết ứng dụng
     * @param body 
     */
    detail(id: string) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: PATH.DETAIL,
            params: {
                id: id
            }
        };
        return this.httpClient.get(options);
    }
}