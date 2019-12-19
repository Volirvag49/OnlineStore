import { Component } from '@angular/core';
import { FormGroup, Validators, FormControl, FormArray, FormBuilder } from '@angular/forms';



@Component({
  selector: 'app-product-test',
    templateUrl: './product-test.component.html',

    
    styles: [
        `.invisible{display:none;}`
    ]
})

export class ProductTestComponent  {
    myForm: FormGroup;
    constructor(private formBuilder: FormBuilder) {

        this.myForm = formBuilder.group({

            "userName": ["Tom", [Validators.required]],
            "userEmail": ["", [Validators.required, Validators.email]],
            "phones": formBuilder.array([
                ["+7", Validators.required]
            ])
        });
    }
    addPhone() {
        (<FormArray>this.myForm.controls["phones"]).push(new FormControl("+7", Validators.required));
    }
    submit() {
        console.log(this.myForm);
    }
}
