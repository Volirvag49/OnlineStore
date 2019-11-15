import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { ApiService } from './api.service';
import { Product } from '../models';
import { map } from 'rxjs/operators';

@Injectable()
export class ProductService {
    constructor(
        private apiService: ApiService
    ) { }

    getAll(): Observable<Product[]> {
        return this.apiService.get('/Product/')
            .pipe(map(data => data.products));
    }

    get(id): Observable<Product> {
        return this.apiService.get('/product/' + id)
            .pipe(map(data => data.product));
    }

    delete(id) {
        return this.apiService.delete('/product/' + id);
    }

    save(product): Observable<Product> {
        // If we're updating an existing item
        if (product.id) {
            return this.apiService.put('/product/' + product.id, { product: product })
                .pipe(map(data => data.product));

            // Otherwise, create a new item
        } else {
            return this.apiService.post('/product/', { product: product })
                .pipe(map(data => data.product));
        }
    }
}
