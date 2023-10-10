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
import { IResource } from '../models/category-resource.model';
@Injectable({
    providedIn: 'root',
})
export class CategoryResourceService {
    constructor(private httpClient: HttpClientService) { }

    /**
     * Lấy danh sách Resource
     * @param options 
     */
    getResources(options: HttpOptions) {
        return this.httpClient.get(options);
    }

    getChildren(item: IResource): IResource[] {
        return [
            {
                id: 3,
                name: 'SM child 1',
                key: 'sm_child_1',
                url: '/sm',
                createdDate: '12/05/2023',
                createdBy: 'anhdkn.os',
                hasChild: true,
            },
            {
                id: 4,
                name: 'SM child child 2',
                key: 'sm_child_2',
                url: '/sm',
                createdDate: '12/05/2023',
                createdBy: 'anhdkn.os',
                hasChild: false,
            },
        ];
    }

    /**
     * Tạo mới Resource
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
     * Chỉnh sửa thông tin Resource
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
     * Xóa Resource
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