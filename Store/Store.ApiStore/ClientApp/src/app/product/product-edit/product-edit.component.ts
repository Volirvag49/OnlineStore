import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { first } from 'rxjs/operators';
import { ProductPutModel, ProductService } from '../../core';


@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html'
})
export class ProductEditComponent implements OnInit {

    product: ProductPutModel;
    errorMessage: string;
    editForm: FormGroup;

    constructor(private formBuilder: FormBuilder,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private location: Location,
        private productService: ProductService) { }

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
            },
                error => {
                this.errorMessage = error;
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
