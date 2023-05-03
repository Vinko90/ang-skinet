import {Component, OnInit} from '@angular/core';
import {Product} from "../../shared/models/product.model";
import {ShopService} from "../shop.service";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html'
})
export class ProductDetailsComponent implements OnInit{
  product?: Product;

  constructor(private shopSrv: ShopService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) this.shopSrv.getProduct(+id).subscribe({
      next: prod => this.product = prod,
      error: error => console.log(error)
    });
  }
}
