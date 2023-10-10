export interface HttpOptions {
    url?: string;
    path?: string;
    body?: any;
    headers?: any;
    params?: any;
    cacheMins?: number;
    isAuthentication?: boolean;
    uuid?: string;
    responseType?:'arraybuffer' | 'blob' | 'json' | 'text';
}

export interface HttpResponse {
    code: number;
    data: any;
    duration: number;
    message: string;
    path: string;
    timestamp: string;
    uuid: string;
}
