import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common'
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";

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
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private location: Location,
        private productService: ProductService) { }

    product: ProductPutModel;

    action: string;

    editForm: FormGroup;

    ngOnInit() {
        let productId = this.activatedRoute.snapshot.paramMap.get('id');
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

    goBack() {
        this.location.back();
    }
}
