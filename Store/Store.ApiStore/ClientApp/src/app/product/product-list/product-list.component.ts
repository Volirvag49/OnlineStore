import { Component, OnInit } from '@angular/core';
import { concatMap, tap } from 'rxjs/operators';
import { Router } from "@angular/router";

import {
    ProductGetModel, ProductService
} from '../../core';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html'
})

export class ProductListComponent implements OnInit {

    products: ProductGetModel[];

    constructor(private router: Router,
        private productService: ProductService) { }

    ngOnInit() {
        this.getAll();
    }

    getAll() {
        this.productService.getAll()
            .subscribe(products => this.products = products);
    }

    addNew(): void {
        this.router.navigate(['product/add']);
    };

    update(productId: string): void {
        console.log(productId);
        this.router.navigate(['product/' +productId +'/edit']);
    };

    delete(productId: string): void {
        this.productService.delete(productId)
            .subscribe(data => {
                this.products = this.products.filter(u => u.id !== productId);
            })
    };
}
