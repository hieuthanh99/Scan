export const environment = {
  production: true,
  hmr: false,
  key: 'cms_prortal',
  base_url: 'http://10.215.254.32:8080',
  hostApi: '',
  keycloak: {
    // Url of the Identity Provider
    issuer: 'https://keycloak-internal-uat.mbbank.com.vn/auth/',
    realm: 'internal',
    client: 'portal-frontend',
    relationShip: 'host-app-service',
    realmManagement: 'realm-management',
  },
  timeOut: 60000,
  smartScanApi: 'http://smart-dev.smartup.com.vn:9933',
  integrateUrls:['/integrate/bpm'],
  canListenUrls:['http://localhost:3000'],
};
