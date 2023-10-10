import {
    HttpClient,
    HttpClientModule,
    HTTP_INTERCEPTORS
} from '@angular/common/http';
import {
    APP_INITIALIZER,
    Injector,
    NgModule
} from '@angular/core';
import {
    createCustomElement
} from '@angular/elements';
import {
    FlexLayoutModule
} from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import {
    BrowserModule
} from '@angular/platform-browser';
import {
    BrowserAnimationsModule
} from '@angular/platform-browser/animations';
import { Router } from '@angular/router';
import {
    environment
} from '@env/environment';
import {
    TranslateLoader,
    TranslateModule
} from '@ngx-translate/core';
import {
    TranslateHttpLoader
} from '@ngx-translate/http-loader';
import {
    ActivityIndicatorModule
} from '@shared-sm';
import { KeycloakAngularModule, KeycloakService } from "keycloak-angular";
import {
    ToastrModule
} from 'ngx-toastr';
import {
    AppRoutingModule
} from './app-routing.module';
import {
    AppComponent
} from './app.component';
import { HomePageComponent } from './features/home-page/home-page/home-page.component';
import { initializer } from './keycloak-initializer';
import {
    materialProviders
} from './material-config';
import {
    NotFoundComponent
} from './not-found/not-found.component';
import { ThemeModule } from './theme/theme.module';
import {
    TranslateLangService
} from './translate-lang.service';
import { MatExpansionModule } from '@angular/material/expansion';
import {
    SharedSMModule
} from '@shared-sm';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NgxMatDatetimePickerModule } from "@angular-material-components/datetime-picker";
import { NgxMatMomentModule } from "@angular-material-components/moment-adapter";
import { CustomHttpIntercepter } from './shared/interceptor/keycloak.interceptor';
import { IntegrateForBpmComponent } from './features/integrate/integrate-for-bpm/integrate-for-bpm.component';
import {AuthIframeService} from "./shared/services/auth-iframe.service";
import {MatTabsModule} from "@angular/material/tabs";
import { BpmCompleteProfileComponent } from './features/integrate/integrate-for-bpm/bpm-complete-profile/bpm-complete-profile.component';
import { BpmProfileCompletedComponent } from './features/integrate/integrate-for-bpm/bpm-profile-completed/bpm-profile-completed.component';
export function TranslateLangServiceFactory(translateLangService: TranslateLangService) {
    return () => translateLangService.load();
}
export function TranslateHttpLoaderFactory(http: HttpClient) {
    const assetsUrl = environment.base_url + '/assets/i18n/';
    return new TranslateHttpLoader(http, assetsUrl, '.json?nocache=' + (new Date()).getTime());
}
@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        ThemeModule,
        HttpClientModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        ActivityIndicatorModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: TranslateHttpLoaderFactory,
                deps: [HttpClient],
            },
        }),
        AppRoutingModule,
        KeycloakAngularModule,
        MatTableModule,
        FlexLayoutModule,
        MatIconModule,
        MatTooltipModule,
        MatExpansionModule,
        SharedSMModule,
        MatMenuModule,
        MatToolbarModule,
        NgxMatDatetimePickerModule,
        NgxMatMomentModule,
        SharedSMModule
    ],
    declarations: [
        AppComponent,
        NotFoundComponent,
        HomePageComponent
    ],
    providers: [
        AuthIframeService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: CustomHttpIntercepter,
            multi: true,
        },
        {
            provide: APP_INITIALIZER,
            useFactory: TranslateLangServiceFactory,
            deps: [TranslateLangService],
            multi: true,
        },
        {
            provide: APP_INITIALIZER,
            useFactory: initializer,
            deps: [KeycloakService, HttpClient, Router, AuthIframeService],
            multi: true,
        },
        materialProviders
    ],
    bootstrap: []
})
export class AppModule {
    constructor(private injector: Injector) { }
    ngDoBootstrap() {
        const ce = createCustomElement(AppComponent, {
            injector: this.injector
        });
        customElements.define('cms_prortal-app-element', ce);
    }
}
