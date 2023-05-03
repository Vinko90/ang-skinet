import {Component, OnInit} from '@angular/core';

import {Product} from "../shared/models/product.model";
import {ShopService} from "./shop.service";

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  public products: Product[] = [];

  constructor(private shopSrv: ShopService) { }

  ngOnInit(): void {
    this.shopSrv.getProducts().subscribe({
      next: response => this.products = response.data,
      error: errorMsg => console.log(errorMsg)
    });
  }

}
