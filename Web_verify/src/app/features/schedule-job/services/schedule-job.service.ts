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
    BehaviorSubject
} from 'rxjs';
import {
    PATH
} from '../constants';
@Injectable({
    providedIn: 'root',
})
export class ScheduleJobService {
    constructor(private httpClient: HttpClientService) {}

  // Lấy thông tin category
  getList(options: HttpOptions) {
    return this.httpClient.post(options);
  }

  // Service add category
  add(body: any) {
    const options: HttpOptions = {
      url: environment.hostApi,
      path: PATH.CREATE,
      body
    };
    return this.httpClient.post(options);
  }

  // Service update category
  update(id: number, body: any) {
    const options: HttpOptions = {
      url: environment.hostApi,
      path: `${PATH.UPDATE}/${id}`,
      body
    };
    return this.httpClient.put(options);
  }

  delete(id: number) {
    const options: HttpOptions = {
      url: environment.hostApi,
      path: `${PATH.DELETE}/${id}`
    };
    return this.httpClient.delete(options);
  }

  startJob(processId: number) {
    const options: HttpOptions = {
      url: environment.hostApi,
      path: `${PATH.START_JOB}?processId=${processId}`,
      body: {
      }
    };
    return this.httpClient.post(options);
  }

  stopJob(processId: number) {
    const options: HttpOptions = {
      url: environment.hostApi,
      path: `${PATH.STOP_JOB}?processId=${processId}`,
      body: {
      }
    };
    return this.httpClient.post(options);
  }
}
