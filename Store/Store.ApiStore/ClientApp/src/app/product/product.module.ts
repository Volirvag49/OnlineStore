import { NgModule } from '@angular/core';
import { SharedModule } from '../shared';
import { ProductRoutingModule } from './product-routing.module';
import { ProductListComponent } from './product-list/product-list.component';


@NgModule({
    imports: [
        ProductRoutingModule,
        SharedModule
    ],
    declarations: [
        ProductListComponent
    ],
    providers: [
    ]
})
export class ProductModule { }
