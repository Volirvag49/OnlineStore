import { Component, Directive, EventEmitter, Input, OnInit, Output, QueryList, ViewChildren }
    from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { concatMap, tap } from 'rxjs/operators';
import { Router } from "@angular/router";
import { NgbdSortableHeader, SortEvent } from '../../shared';

import {
    ProductGetModel, ProductService, RequesthModel, ResponceModel
} from '../../core';


@Component({
  selector: 'app-product-list',
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.css']
})

export class ProductListComponent implements OnInit {

    requestModel: RequesthModel;
    responceModel: ResponceModel<ProductGetModel>;

    searchForm: FormGroup;

    @ViewChildren(NgbdSortableHeader) headers: QueryList<NgbdSortableHeader>;

    constructor(private router: Router,
        private productService: ProductService,
        private formBuilder: FormBuilder) { }

    ngOnInit() {
        this.searchForm = this.formBuilder.group({
            searchString: ['', Validators.required],
            searchSelection: ['Name', Validators.required]
        });

        this.requestModel = {
            currentPage: 1,
            pageSize: 5,
            searchSelection: null,
            searchString: null,
            sortOrder: null,
            byDescending: false
        };

        this.getPaged();
    }

    onSearch() {
        this.requestModel.searchString = this.searchForm.value.searchString;
        this.requestModel.searchSelection = this.searchForm.value.searchSelection;
        this.getPaged();
    }

    onSort({ column, direction }: SortEvent) {

        // resetting other headers
        this.headers.forEach(header => {
            if (header.sortable !== column) {
                header.direction = '';
            }
        });

        if (direction === '')
            column = null;

        this.requestModel.sortOrder = column;
        this.requestModel.byDescending = (direction === 'desc');

        this.getPaged();
    }

    getPaged() {
        this.productService.getPaged(this.requestModel)
            .subscribe(data => {
                this.responceModel = data;
            });
    }

    changePage(page: number) {
        this.requestModel.currentPage = page;
        this.getPaged();
    }

    changePageSize(size: number) {
        this.requestModel.pageSize = +size;
        this.requestModel.currentPage = 1;
        this.getPaged();
    }

    addNew(): void {
        this.router.navigate(['product/add']);
    };

    update(productId: string): void {
        this.router.navigate(['product/' + productId +'/edit']);
    };

    delete(productId: string): void {

        var comfirmResult = confirm("remove?");

        if (comfirmResult == false)
            return;

        this.productService.delete(productId)
            .subscribe(data => {
                this.getPaged();
            })
    };
}
