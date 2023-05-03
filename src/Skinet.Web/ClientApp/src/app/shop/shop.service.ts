import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";

import {Pagination} from "../shared/models/pagination.model";
import {Product} from "../shared/models/product.model";

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  constructor(private http: HttpClient) { }

  getProducts() {
    return this.http.get<Pagination<Product[]>>('products?pageSize=50');
  }
}
