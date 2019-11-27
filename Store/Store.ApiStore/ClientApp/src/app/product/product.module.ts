import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '../shared';
import { ProductAddComponent } from './product-add/product-add.component';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';
import { ProductRoutingModule } from './product-routing.module';



@NgModule({
    imports: [
        ProductRoutingModule,
        SharedModule,
        NgbModule
    ],
    declarations: [
        ProductListComponent,
        ProductAddComponent,
        ProductEditComponent
    ],
    providers: [
    ]
})
export class ProductModule { }
