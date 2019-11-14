import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import {
    ApiService,
    ProductService
} from './services';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
    ],
    providers: [
        ApiService,
        ProductService
    ],
})
export class CoreModule { }
