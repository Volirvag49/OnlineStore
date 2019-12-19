import { NgModule } from '@angular/core';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SharedModule } from '../shared';
import { ProductAddComponent } from './product-add/product-add.component';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { ProductListComponent } from './product-list/product-list.component';

import { ProductTestComponent } from './product-test/product-test.component';
import { BoldDirective } from './product-test/bold.directive';
import { WhileDirective } from './product-test/while.directive';
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
        ProductEditComponent,
        ProductTestComponent, BoldDirective, WhileDirective
    ],
    providers: [
    ]
})
export class ProductModule { }
