import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html'
})
export class TestErrorComponent {
  validationErrors: string[] = [];

  constructor(private http: HttpClient) {
  }

  get404Error() {
    this.http.get('products/44444').subscribe({
      next: res => console.log(res),
      error: err => console.log(err)
    });
  }

  get500Error() {
    this.http.get('buggy/servererror').subscribe({
      next: res => console.log(res),
      error: err => console.log(err)
    });
  }

  get400Error() {
    this.http.get('buggy/badrequest').subscribe({
      next: res => console.log(res),
      error: err => console.log(err)
    });
  }

  get400ValidationError() {
    this.http.get('products/fortytwo').subscribe({
      next: res => console.log(res),
      error: err => {
        console.log(err);
        this.validationErrors = err.errors;
      }
    });
  }
}
