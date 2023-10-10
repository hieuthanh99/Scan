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
export class CategoryScopeService {
    constructor(private httpClient: HttpClientService) { }
    getListScope(page, size, searchText) {
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

    createScope(template) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.CREATE}`,
            body: template
        };

        return this.httpClient.post(options);
    }

    updateScope(template) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.UPDATE}`,
            body: template
        };

        return this.httpClient.post(options);
    }

    deleteScope(id) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.DELETE}/${id}`,
        };

        return this.httpClient.delete(options);
    }
}