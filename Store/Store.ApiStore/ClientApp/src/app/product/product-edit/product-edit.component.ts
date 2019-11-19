import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

import {
    ProductPutModel, ProductService
} from '../../core';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html'
})
export class ProductEditComponent implements OnInit {

    constructor(private formBuilder: FormBuilder,
        private router: Router,
        private productService: ProductService) { }

    product: ProductPutModel;

    editForm: FormGroup;

    ngOnInit() {
        let productId = window.localStorage.getItem("id");
        if (!productId) {
            alert("Invalid action.")
            this.router.navigate(['product']);
            return;
        }

        this.product = {
            id: null,
            name: null,
            description: null,
            photoUrl: null,
            price: null
        }

        this.editForm = this.formBuilder.group({
            id: [this.product.id, Validators.required],
            name: [this.product.name, Validators.required],
            description: [this.product.description, Validators.required],
            photoUrl: [this.product.photoUrl, Validators.required],
            price: [this.product.price, Validators.required]
        });

        this.productService.get(productId)
            .subscribe(data => {
                this.editForm.setValue(data);
            });
    }

    onSubmit() {
        this.productService.update(this.editForm.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.router.navigate(['product']);
                },
                error => {
                    alert(error);
                });
    }
}
