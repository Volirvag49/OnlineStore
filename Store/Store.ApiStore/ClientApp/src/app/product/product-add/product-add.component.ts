import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

import {
    Product, ProductService
} from '../../core';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html'
})
export class ProductAddComponent implements OnInit {

    constructor(private formBuilder: FormBuilder,
        private router: Router, 
        private productService: ProductService) { }

    addForm: FormGroup;

    ngOnInit() {
        this.addForm = this.formBuilder.group({
            id: null,
            name: ['', Validators.required],
            description: ['', Validators.required],
            photoUrl: ['', Validators.required],
            price: ['', Validators.required]
        });
    }

    onSubmit() {
        this.productService.save(this.addForm.value)
            .subscribe(data => {
                this.router.navigate(['product']);
            });
    }

}
