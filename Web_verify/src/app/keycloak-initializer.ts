import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {environment} from '@env/environment';
import {CURRENCY_APP_CODE, CURRENCY_CLIENT_ID, CURRENCY_KEYCLOAK_ID, USER_NAME} from '@shared-sm';
import {KeycloakEventType, KeycloakService} from 'keycloak-angular';
import {first, fromEvent, lastValueFrom, timeout} from "rxjs";
import {AuthIframeService} from "./shared/services/auth-iframe.service";

export function initializer(keycloak: KeycloakService, http: HttpClient, router: Router, authIframeService: AuthIframeService): () => Promise<any> {
  return (): Promise<any> => {
    async function handleKeyCloak(resolve: (value: (PromiseLike<any> | any)) => void, reject: (reason?: any) => void) {
      try {
        const jwt = localStorage.getItem('token')
        if (jwt) {
          resolve(true);
        } else {
          keycloak.keycloakEvents$.subscribe(event => {
            console.log('event.type', event.type)
            if (event.type === KeycloakEventType.OnAuthSuccess) {
              keycloak.loadUserProfile().then(async profile => {
                if (localStorage.getItem(USER_NAME) !== profile.username) {
                  localStorage.setItem(USER_NAME, profile.username);
                  //router.navigateByUrl('/');
                }
              });
              if (localStorage.getItem(CURRENCY_APP_CODE) == null) {
                router.navigateByUrl('cms_prortal/home-page');
              }
            }
          });

          await keycloak.init({
            config: {
              url: environment.keycloak.issuer,
              realm: environment.keycloak.realm,
              clientId: environment.keycloak.client,
            },
            loadUserProfileAtStartUp: true,
            initOptions: {
              onLoad: 'login-required',
              checkLoginIframe: false,
              // checkLoginIframeInterval: 5,
            },
            bearerPrefix: 'Bearer',
          });
          resolve(true);
        }

      } catch (error) {
        reject(error);
      }
    }

    async function getEventData(listenEvent): Promise<string> {
      const messageEvent = await lastValueFrom(listenEvent);
      if (!(messageEvent instanceof MessageEvent)) {
        return null;
      }
      console.log(messageEvent);
      const indexOrigin = environment.canListenUrls.findIndex(url => url.toUpperCase() === messageEvent.origin.toUpperCase());
      if (indexOrigin < 0) {
        return null;
      }
      return messageEvent.data;
    }

    return new Promise(async (resolve, reject) => {
      const index = environment.integrateUrls.findIndex(url => url.toUpperCase() === window.location.pathname.toUpperCase());
      console.log('initializer', index);
      if (index < 0) {
        await handleKeyCloak(resolve, reject);
        return;
      }
      // console.log('localStorage.clear()');
      // localStorage.clear();
      let listenEvent = await fromEvent(window, 'message')
        .pipe(
          timeout(10000),
          first(),
        );
      const confirmMessage = await getEventData(listenEvent);
      console.log('confirmMessage', confirmMessage);
      if (!confirmMessage || confirmMessage !== 'AreYouReady?') {
        await handleKeyCloak(resolve, reject);
        return;
      }
      listenEvent = await fromEvent(window, 'message')
        .pipe(
          timeout(10000),
          first(),
        );
      console.log(window.parent);
      window.parent.postMessage('OkReady!', '*');
      const bpmInputData = await getEventData(listenEvent);
      console.log('bpmInputData', bpmInputData);
      if (!bpmInputData) {
        await handleKeyCloak(resolve, reject);
        return;
      }
      const jsonBpmInputData = JSON.parse(bpmInputData);
      localStorage.setItem('token', jsonBpmInputData.token.toString());
      localStorage.setItem('username', jsonBpmInputData.username.toString());
      localStorage.setItem(CURRENCY_APP_CODE, 'Integrate');
      localStorage.setItem(CURRENCY_KEYCLOAK_ID, environment.keycloak.realm);
      localStorage.setItem(CURRENCY_CLIENT_ID, environment.keycloak.client);
      authIframeService.tokenSubject.next(true);
      await handleKeyCloak(resolve, reject);
    });
  }
}
