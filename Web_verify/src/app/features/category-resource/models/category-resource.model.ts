export interface IResource {
    stt?: number;
    id: number;
    key: string;
    name: string;
    url: string;
    createdDate: string;
    createdBy: string;
    modifiedDate?: string;
    modifiedBy?: string;
    hasChild: boolean;
    childs?: IResource[];
}

export interface TreeNode {
    item: IResource;
    level: number;
    expandable: boolean;
    isLoading : boolean;
}