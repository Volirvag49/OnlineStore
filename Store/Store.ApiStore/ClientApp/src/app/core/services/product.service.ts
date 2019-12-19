import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { ProductGetModel, ProductPostModel, ProductPutModel, RequesthModel, ResponceModel } from '../models';
import { map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

@Injectable()
export class ProductService {
    constructor(
        private apiService: ApiService
    ) { }

    getAll(): Observable<ProductGetModel[]> {
        return this.apiService.get('/products/')
            .pipe(map(data => data));
    }

    get(id: string): Observable<ProductGetModel> {
        return this.apiService.get('/products/' + id)
            .pipe(map(data => data));
    }

    getPaged(request: RequesthModel): Observable<ResponceModel<ProductGetModel>> {
        return this.apiService.post('/products/search', request)
            .pipe(map(data => data));
    }

    create(product: ProductPostModel): Observable<ProductPostModel> {

        // Otherwise, create a new item
        return this.apiService.post('/products', product)
            .pipe(map(data => data));
    }

    update(product: ProductPutModel): Observable<ProductPutModel> {
        // updating an existing item
        if (product.id) {
            return this.apiService.put('/products/', product)
                .pipe(map(data => data));
        }
    }

    delete(id: string) {
        return this.apiService.delete('/products/' + id);
    }

    getImageBaseURl(imageUrl: string): string {
        return environment.api_url + '/Products/images?fileUrl=' + imageUrl;
    }
}
