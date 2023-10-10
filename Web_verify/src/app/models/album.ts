import { image } from "./image";

export interface album {
    id: string,
    name: string,
    path: string,
    numberOfImage: number,
    createdDate: string,
    modifiedDate: string,
    createdByUser: string,
    modifiedByUser: String,
    ownerUser: string,
    thumbnailImage: string,
    priorityDisplay: string
    images: image[],
    customerType: string,
 }