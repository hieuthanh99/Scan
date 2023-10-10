export const environment = {
    production: true,
    hmr: false,
    key: 'cms_prortal',
    base_url: 'http://10.1.27.129:9116',
    hostApi: 'http://10.215.254.52:8080',
    s3Sever:'http://192.168.1.171:9000/',
    bucketName:'ecm-scan-capture-mcrs',
    keycloak: {
        // Url of the Identity Provider
        issuer: 'https://keycloak-internal-uat.mbbank.com.vn/auth/',
        realm: 'internal',
        client: 'portal-frontend',
        relationShip: 'host-app-service',
        realmManagement: 'realm-management',
    },
    smartScanApi: 'http://10.1.27.129:9933',
    integrateUrls:['/integrate/bpm'],
    canListenUrls:['http://localhost:3000'],
    timeOut: 60000,
};
