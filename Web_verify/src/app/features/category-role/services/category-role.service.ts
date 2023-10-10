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
export class CategoryRoleService {
    constructor(private httpClient: HttpClientService) {}
    getListRole (page, size, searchText) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.LIST}`,
            params: {
                page,
                size,
                searchText,
            }
        };
      
        return this.httpClient.get(options);
    }

    getRoleById (id) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.GET}/${id}`,
        };
      
        return this.httpClient.get(options);
    }

    createRole (template) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.CREATE}`,
            body: template
        };
      
        return this.httpClient.post(options);
    }

    updateRole (template) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.UPDATE}`,
            body: template
        };
      
        return this.httpClient.post(options);
    }

    deleteRole(id) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.DELETE}/${id}`,
        };
      
        return this.httpClient.delete(options);
    }
}