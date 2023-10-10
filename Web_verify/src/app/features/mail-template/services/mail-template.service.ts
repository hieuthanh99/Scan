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
export class MailTemplateService {
    constructor(private httpClient: HttpClientService) { }

    
    getListMailTemplate (page, size, templateId, subject) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.LIST}`,
            params: {
                page,
                size,
                templateId,
                subject
            }
        };
      
        return this.httpClient.get(options);
    }

    createMailTemplate (template) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.CREATE}`,
            body: template
        };
      
        return this.httpClient.post(options);
    }

    updateMailTemplate (template) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.UPDATE}`,
            body: template
        };
      
        return this.httpClient.post(options);
    }

    deleteMailTemplate (id) {
        const options: HttpOptions = {
            url: environment.hostApi,
            path: `${PATH.DELETE}/${id}`,
        };
      
        return this.httpClient.delete(options);
    }
}