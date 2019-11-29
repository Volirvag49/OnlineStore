import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common'
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

import {
    ProductPostModel, ProductService
} from '../../core';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html'
})
export class ProductAddComponent implements OnInit {

    constructor(private formBuilder: FormBuilder,
        private router: Router,
        private location: Location,
        private productService: ProductService) { }

    newProduct: ProductPostModel;
    addForm: FormGroup;
    errorMessage: string;

    ngOnInit() {
        this.newProduct = {
            name: null,
            description: null,
            photoUrl: null,
            price: null
        }

        this.addForm = this.formBuilder.group({
            name: [this.newProduct.name, Validators.required],
            description: [this.newProduct.description, Validators.required],
            photoUrl: [this.newProduct.photoUrl, Validators.required],
            price: [this.newProduct.price, Validators.required]
        });
    }

    onSubmit() {
        this.productService.create(this.addForm.value)
            .subscribe(data => {
                this.router.navigate(['product']);
            },
            error => {
                this.errorMessage = error;
            });
    }

    goBack() {
        this.location.back();
    }

}
