export default interface IUser {
    stt?: number;
    id: number;
    username: string;
    name: string;
    email: string;
    phone: string;
    siteCode: string;
    userT24?: string,
    status: string,
    activeDateFrom?: string,
    activeDateTo?: string,
    createdDate: string;
    createdBy: string;
    modifiedDate?: string;
    modifiedBy?: string;
}