export interface PaginationDataBaseDto<T> {
    code?: number;
    data: PaginationBaseDto<T>;
}

export interface PaginationBaseDto<T> {
    items: T[];
    pageSize?: number;
    pageNum?: number;
    totalPage?: number;
    totalCount?: number;
}
