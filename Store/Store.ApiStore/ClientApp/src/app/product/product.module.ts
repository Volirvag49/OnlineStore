import { NgModule } from '@angular/core';
import { SharedModule } from '../shared';
import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductAddComponent } from './product-add/product-add.component';


@NgModule({
    imports: [
        ProductRoutingModule,
        SharedModule
    ],
    declarations: [
        ProductListComponent,
        ProductAddComponent
    ],
    providers: [
    ]
})
export class ProductModule { }
