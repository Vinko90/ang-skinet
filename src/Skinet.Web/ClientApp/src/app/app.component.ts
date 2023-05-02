import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Product} from "./shared/models/product.model";
import {Pagination} from "./shared/models/pagination.model";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

  public products: Product[] = [];

  constructor(private http: HttpClient) {
  }

  ngOnInit(): void {
    this.http.get<Pagination<Product[]>>('products').subscribe({
      next: response => this.products = response.data,
      error: errorMsg => console.log(errorMsg),
      complete: () => {
        console.log('Request Completed');
      }
    });
  }
}
