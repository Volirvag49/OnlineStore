import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-error-message',
    templateUrl: './error.message.component.html'
})
export class ErrorMessageComponent {
  formattedError: any;

  @Input()
  set errors(error: any) {
      this.formattedError = error;
  }
    get errorMessage() { return this.formattedError; }
}
