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
export class CategoryPublicService {
    constructor(private httpClient: HttpClientService) { }

    // Lấy thông tin category
    getListCategory(options: HttpOptions) {
        return this.httpClient.get(options);
    }

    // Service add category
    addCategory(body: any) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: PATH.CREATE,
            body
        };
        return this.httpClient.post(options);
    }

    // Service update category
    updateCategory(id: number, body: any) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.UPDATE}`,
            body
        };
        return this.httpClient.put(options);
    }

  deleteCategory(id: number) {
    const options: HttpOptions = {
      url: environment.hostApi,
      path: `${PATH.UPDATE}/${id}`
    };
    return this.httpClient.delete(options);
  }
}
