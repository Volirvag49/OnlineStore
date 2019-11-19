import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from './api.service';
import { ProductGetModel, ProductPostModel, ProductPutModel } from '../models';
import { map } from 'rxjs/operators';

@Injectable()
export class ProductService {
    constructor(
        private apiService: ApiService
    ) { }

    getAll(): Observable<ProductGetModel[]> {
        return this.apiService.get('/Product/')
            .pipe(map(data => data));
    }

    get(id: string): Observable<ProductGetModel> {
        return this.apiService.get('/product/' + id)
            .pipe(map(data => data));
    }

    create(product: ProductPostModel): Observable<ProductPostModel> {

        // Otherwise, create a new item
        return this.apiService.post('/product/', product)
            .pipe(map(data => data));
    }

    update(product: ProductPutModel): Observable<ProductPutModel> {
        // updating an existing item
        if (product.id) {
            return this.apiService.put('/product/', product)
                .pipe(map(data => data));
        }
    }

    delete(id: string) {
        return this.apiService.delete('/product/' + id);
    }
}
