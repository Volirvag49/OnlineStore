export interface ProductPostModel {
    name: string;
    description: string;
    photoUrl: string;
    price: number;
}

export interface ProductPutModel {
    id: string;
    name: string;
    description: string;
    photoUrl: string;
    price: number;
}

export interface ProductGetModel {
    id: string;
    name: string;
    description: string;
    photoUrl: string;
    price: number;
}
