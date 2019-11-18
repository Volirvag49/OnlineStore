import { Component, OnInit } from '@angular/core';
import { concatMap, tap } from 'rxjs/operators';
import { Router } from "@angular/router";

import {
    Product, ProductService
} from '../../core';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html'
})

export class ProductListComponent implements OnInit {

    products: Product[];

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
}
