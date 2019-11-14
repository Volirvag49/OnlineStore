import { Injectable } from '@angular/core';
import { HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ApiService } from './api.service';
import { Product } from '../models';
import { map } from 'rxjs/operators';

@Injectable()
export class ProductService {
    constructor(
        private apiService: ApiService
    ) { }

    get(id): Observable<Product> {
        return this.apiService.get('/employee/' + id)
            .pipe(map(data => data.product));
    }

    delete(id) {
        return this.apiService.delete('/employee/' + id);
    }

    save(product): Observable<Product> {
        // If we're updating an existing item
        if (product.id) {
            return this.apiService.put('/employee/' + product.id, { product: product })
                .pipe(map(data => data.product));

            // Otherwise, create a new item
        } else {
            return this.apiService.post('/employee/', { product: product })
                .pipe(map(data => data.product));
        }
    }
}
