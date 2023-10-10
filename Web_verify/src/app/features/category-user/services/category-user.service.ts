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
@Injectable({
    providedIn: 'root',
})
export class CategoryUserService {
    constructor(private httpClient: HttpClientService) { }

    /**
     * Lấy danh sách người dùng
     * @param options 
     */
    getUsers(options: HttpOptions) {
        return this.httpClient.get(options);
    }

    /**
     * Tạo mới người dùng
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
     * Chỉnh sửa thông tin người dùng
     * @param body 
     */
    update(body: any) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: PATH.UPDATE,
            body
        };
        return this.httpClient.post(options);
    }

    /**
     * Xóa người dùng
     * @param body 
     */
    delete(body: any) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: PATH.DELETE,
            body
        };
        return this.httpClient.post(options);
    }
}