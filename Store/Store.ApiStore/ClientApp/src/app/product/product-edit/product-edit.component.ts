import { Location } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { first } from 'rxjs/operators';
import { ProductPutModel, ProductService, FileToUploadPutModel, FileToUploadGetModel } from '../../core';
import { FileUploadComponent } from '../../shared';


@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html'
})
export class ProductEditComponent implements OnInit {

    product: ProductPutModel;
    editForm: FormGroup;
    errorMessage: string;

    @ViewChild(FileUploadComponent, { static: false })
    private fileUploadComponent: FileUploadComponent;


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
            image: null,
            price: null
        }
        this.editForm = this.formBuilder.group({
            id: [this.product.id, Validators.required],
            name: [this.product.name, Validators.required],
            description: [this.product.description, Validators.required],
            image: [null],
            price: [this.product.price, Validators.required]
        });

        this.productService.get(productId)
            .subscribe(data => {
                this.product = data;
                this.editForm.setValue(this.product);
                this.showPriview(data.image);
            },
            error => {
            this.errorMessage = error;
            });

    }

    onSubmit() {

        let updatedProduct = (this.editForm.value as ProductPutModel);

        console.log(updatedProduct);
        updatedProduct.image = this.getFilePutModel(updatedProduct.image);
        this.productService.update(updatedProduct)
            .pipe(first())
            .subscribe(
                data => {
                    this.router.navigate(['product']);
                },
                error => {
                    this.errorMessage = error;
                });
    }

    getFilePutModel(file: any): FileToUploadPutModel {

        console.log("getfile");
        if (file == null)
            return null;
        console.log(this.product);

        let fileToUpload = new FileToUploadPutModel;

        if (this.product.image != null && this.product.image.id != null)
          fileToUpload.id = this.product.image.id;

        fileToUpload.fileName = file.name;
        fileToUpload.fileSize = file.size;
        fileToUpload.fileType = file.type;
        fileToUpload.lastModified = file.lastModifiedDate;
        fileToUpload.fileAsBase64 = file.fileAsBase64;

        return fileToUpload;


    }

    showPriview(model: FileToUploadGetModel) {
        this.fileUploadComponent.showPreviewFromModel(model);
    }


    goBack() {
        this.location.back();
    }
}
