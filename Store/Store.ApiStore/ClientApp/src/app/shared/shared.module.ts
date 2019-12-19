import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbdSortableHeader } from './directives';
import { ErrorMessageComponent, FileUploadComponent } from './components';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        RouterModule
    ],
    declarations: [
        ErrorMessageComponent,
        FileUploadComponent,
        NgbdSortableHeader

    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        RouterModule,
        ErrorMessageComponent,
        FileUploadComponent,
        NgbdSortableHeader

    ]
})
export class SharedModule { }
