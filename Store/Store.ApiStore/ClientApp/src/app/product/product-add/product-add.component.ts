import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common'
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

import {
    ProductPostModel, FileToUploadPostModel, ProductService
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

    initProduct: ProductPostModel;
    addForm: FormGroup;
    imageURL: string;
    errorMessage: string;

    theFile: any = null;
    messages: string[] = [];

    ngOnInit() {
        this.initProduct = {
            name: "asdasd",
            description: "asdasd",
            image: null,
            price: 313
        }

        this.addForm = this.formBuilder.group({
            name: [this.initProduct.name, Validators.required],
            description: [this.initProduct.description, Validators.required],
            image: [null],
            price: [this.initProduct.price, Validators.required]
        });
    }

    onSubmit() {
        let createdProduct = (this.addForm.value as ProductPostModel);

        createdProduct.image = this.getFilePostModel(createdProduct.image);
        console.log('createdProduct:');
        console.log(createdProduct);
        this.productService.create(createdProduct)
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

    getFilePostModel(file: any): FileToUploadPostModel {
        if (file == null)
            return null;

        let fileToUpload = new FileToUploadPostModel;
        console.log(file);
        fileToUpload.fileName = file.name;
        fileToUpload.fileSize = file.size;
        fileToUpload.fileType = file.type;
        fileToUpload.lastModified = file.lastModifiedDate;
        fileToUpload.fileAsBase64 = file.fileAsBase64;

        return fileToUpload;
    }

}
