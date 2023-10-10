export default interface ISystem {
    stt?: number;
    id: number;
    appCode: string;
    appName: string;
    isKeycloak: boolean;
    secretKey: string;
    status:boolean;
    clientId: string;
    isShowMenu: boolean;
    type: string;
    subDomain: string;
    elementName: string;
    description: string;
    image: string;
    keycloakId: string;
    createdDate: string;
    createdBy: string;
    updatedDate?: string;
    updatedBy?: string;
}