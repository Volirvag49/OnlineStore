export interface ProductPostModel {
    name: string;
    description: string;
    image: FileToUploadPostModel;
    price: number;
}

export interface ProductPutModel {
    id: string;
    name: string;
    description: string;
    image: FileToUploadPutModel;
    price: number;
}

export interface ProductGetModel {
    id: string;
    name: string;
    description: string;
    image: FileToUploadGetModel;
    price: number;
}

export class FileToUploadPostModel {
    fileName: string = "";
    fileSize: number = 0;
    fileType: string = "";
    lastModified: Date = null;
    fileAsBase64: string = "";
}

export class FileToUploadGetModel {
    id: string;
    fileName: string = "";
    fileUrl: string = "";
    fileSize: number = 0;
    fileType: string = "";
    lastModified: Date = null;
    fileAsBase64: string = "";
}

export class FileToUploadPutModel {
    id: string;
    fileName: string = "";
    fileSize: number = 0;
    fileType: string = "";
    lastModified: Date = null;
    fileAsBase64: string = "";
}
