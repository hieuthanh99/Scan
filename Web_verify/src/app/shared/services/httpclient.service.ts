import { HttpClient, HttpParameterCodec } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { v4 as uuidv4 } from 'uuid';
import { CURRENCY_APP_CODE, CURRENCY_KEYCLOAK_ID, CURRENCY_CLIENT_ID } from '../constants';
import { HttpOptions } from '../models/request.base.dto';
import { LocalStoreManagerService } from './local-store-manager.service';
export enum Verbs {
    GET = 'GET',
    PUT = 'PUT',
    POST = 'POST',
    DELETE = 'DELETE'
}
export class HttpUrlEncodingCodec implements HttpParameterCodec {
    encodeKey(k: string): string { return standardEncoding(k); }
    encodeValue(v: string): string { return standardEncoding(v); }
    decodeKey(k: string): string { return decodeURIComponent(k); }
    decodeValue(v: string) { return decodeURIComponent(v); }
}
function standardEncoding(v: string): string {
    return encodeURIComponent(v);
}
@Injectable({ providedIn: 'root' })
export class HttpClientService {

    constructor(
        private http: HttpClient,
        private localStore: LocalStoreManagerService,
        private router: Router
    ) {
    }

    get<T>(options: HttpOptions): Observable<T> {
        return this.httpCall(Verbs.GET, options);
    }

    delete<T>(options: HttpOptions): Observable<T> {
        return this.httpCall(Verbs.DELETE, options);
    }

    post<T>(options: HttpOptions): Observable<T> {
        return this.httpCall(Verbs.POST, options);
    }

    put<T>(options: HttpOptions): Observable<T> {
        return this.httpCall(Verbs.PUT, options);
    }

    httpCall<T>(verb: Verbs, options: HttpOptions): Observable<T> {
        // Setup default values
        options.body = options.body ?? null;
        options.headers = options.headers ?? {};
        if (localStorage.getItem(CURRENCY_APP_CODE) && localStorage.getItem(CURRENCY_KEYCLOAK_ID)) {
            const header = {
                ...options.headers,
                clientMessageId: uuidv4(),
                appCode: localStorage.getItem(CURRENCY_APP_CODE),
                keycloakId: localStorage.getItem(CURRENCY_KEYCLOAK_ID),
                keycloakClientId: localStorage.getItem(CURRENCY_CLIENT_ID),
            }
            options.headers = this.localStore.getData('token') ? { ...header, Authorization: `Bearer ${this.localStore.getData('token')}` } : header
        } else {
            options.headers = {
                ...options.headers,
                clientMessageId: uuidv4(),
            };
        }

        return this.http.request<T>(verb, `${options.url}/${options.path}`, {
            body: options.body,
            headers: options.headers,
            params: options.params ?? null
        });
    }

    download(verb: Verbs, options: HttpOptions) {
        options.body = options.body ?? null;
        options.headers = options.headers ?? {};
        return this.http.request(verb, `${options.url}/${options.path}`, {
            headers: options.headers,
            observe: 'response',
            responseType: 'blob',
            params: options.params ?? null
        });
    }

    upload(options: HttpOptions) {
        options.body = options.body ?? null;
        options.headers = options.headers ?? {};
        return this.http.post<any>(`${options.url}/${options.path}`, options.body, {
            headers: options.headers,
            observe: 'response',
            params: options.params ?? null
        });
    }

}


