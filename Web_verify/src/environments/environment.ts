export const environment = {
    production: false,
    hmr: true,
    key: 'cms_prortal',
    base_url: 'http://localhost:4200',
    hostApi: 'http://localhost:8082',
    smartScanApi: 'http://dev.smartsolutionvn.net:9933',
    s3Sever:'http://dev.smartsolutionvn.net:9000/',
    bucketName:'ecm-scan-capture-mcrs',
    keycloak: {
        // Url of the Identity Provider
        issuer: 'http://keycloak.smartsolutionvn.net:8000/',
        realm: 'ecm',
        client: 'ecm-smart-capture-bpm',
        relationShip: 'host-app-service',
        realmManagement: 'realm-management',
    },
    integrateUrls:['/integrate/bpm'],
    canListenUrls:['http://localhost:3000'],
    timeOut: 60000,
};

