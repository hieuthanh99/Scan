import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable, Subject, throwError } from 'rxjs';
import { catchError, takeUntil, timeout } from 'rxjs/operators';
import { KeycloakEventType, KeycloakService } from 'keycloak-angular';
import { MessageSeverity, ToastMbService } from '@shared-sm';
@Injectable()
export class CustomHttpIntercepter implements HttpInterceptor {
    private readonly REMOVE_TOKEN_AUTH = 401;
    private readonly ERROR_500 = 500;
    public ngUnsubscribe = new Subject();
    constructor(
        private keyCloakService: KeycloakService,
        private toastr: ToastMbService,
    ) {

        this.keyCloakService?.keycloakEvents$.subscribe(async (res) => {
            console.log('res', res);
            if (res?.type === KeycloakEventType.OnAuthRefreshError) {
                //this.userService.logout();
            } else if (res?.type === KeycloakEventType.OnTokenExpired) {
                // Update token khi hết hạn
                this.keyCloakService?.updateToken();
            }
        });
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const httpClientTimeOut = environment.timeOut;
        return next.handle(req)
            .pipe(
                timeout(httpClientTimeOut),
                catchError((error: any) => this.handleErrorReq(error, req))
            );
    }

    private handleErrorReq(error: any, req?: HttpRequest<any>): Observable<never> {
        // Kiểm tra mã lỗi
        switch (error.status) {
            // Lỗi hết token
            case this.REMOVE_TOKEN_AUTH:
                break;
            // Lỗi response trả về 500
            case this.ERROR_500:
                console.log('ERROR_500', error)
                const error500 = error.error && error.error?.base ? error.error?.base?.message : 'Có lỗi xảy ra. Vui lòng liên hệ CNTT để được hỗ trợ.';
                // const uuid500 = error.error && error.error?.uuid ? error.error?.uuid : null;
                // const messageHtml = uuid500 ? `${error500} <br> ${uuid500}` : `${error500}`
                this.toastr.showToastr(
                    error500,
                    'Thông báo',
                    MessageSeverity.error
                );

                break;
            // Các lỗi còn lại
            default:
                console.log('default', error)
                const message = error.error && error.error?.base ? error.error?.base?.message : 'Có lỗi xảy ra. Vui lòng liên hệ CNTT để được hỗ trợ.';
                // const uuid = error.error && error.error?.uuid ? error.error?.uuid : null;
                // const messageHandle = uuid ? `${message} <br> ${uuid}` : `${message}`
                this.toastr.showToastr(
                    message,
                    'Thông báo',
                    MessageSeverity.error
                );
                break;
        }
        return throwError(error);
    }
}