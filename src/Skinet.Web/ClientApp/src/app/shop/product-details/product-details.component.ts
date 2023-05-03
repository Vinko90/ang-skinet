import {Component, OnInit} from '@angular/core';
import {Product} from "../../shared/models/product.model";
import {ShopService} from "../shop.service";
import {ActivatedRoute} from "@angular/router";
import {BreadcrumbService} from "xng-breadcrumb";

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html'
})
export class ProductDetailsComponent implements OnInit{
  product?: Product;

  constructor(private shopSrv: ShopService, private activatedRoute: ActivatedRoute, private bcService: BreadcrumbService) {
    this.bcService.set('@productDetails', ' '); //Set empty because of fake API delay
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) this.shopSrv.getProduct(+id).subscribe({
      next: prod => {
        this.product = prod;
        this.bcService.set('@productDetails', prod.name);
      },
      error: error => console.log(error)
    });
  }
}
