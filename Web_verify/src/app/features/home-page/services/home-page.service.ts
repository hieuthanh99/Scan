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
import { BehaviorSubject, Observable } from 'rxjs';
@Injectable({
    providedIn: 'root',
})
export class HomePageService {
    private appCode$: BehaviorSubject<any> = new BehaviorSubject(null);
    constructor(private httpClient: HttpClientService) { }

    
    getAppCode(): Observable<any> {
        return this.appCode$.asObservable();
    }

    setAppCode(data: any) {
        this.appCode$.next(data);
    }

    /**
     * Lấy danh sách ứng dụng
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
     * Chỉnh sửa thông tin ứng dụng
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
     * Xóa ứng dụng
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