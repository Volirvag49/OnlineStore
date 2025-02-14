import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpTokenInterceptor } from './interceptors/http.token.interceptor';
import { HttpErrorInterceptor } from './interceptors/http.error.interceptor';

import {
    ApiService,
    ProductService,
} from './services';

@NgModule({
  declarations: [],
  imports: [
    CommonModule
    ],
    providers: [
        { provide: HTTP_INTERCEPTORS, useClass: HttpTokenInterceptor, multi: true },
        { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
        ApiService,
        ProductService,
    ],
})
export class CoreModule { }
