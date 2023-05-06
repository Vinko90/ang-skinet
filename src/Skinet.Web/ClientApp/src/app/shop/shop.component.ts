import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';

import {Product} from "../shared/models/product.model";
import {ShopService} from "./shop.service";
import {Brand} from "../shared/models/brand.model";
import {Type} from "../shared/models/type.model";
import {ShopParams} from "../shared/models/shopParams.model";

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  @ViewChild('search') searchInput?: ElementRef;
  public products: Product[] = [];
  public brands: Brand[] = [];
  public types: Type[] = [];
  public shopParams: ShopParams;
  public totalCount: number = 0;
  public sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' }
  ];

  constructor(private shopSrv: ShopService) {
    this.shopParams = shopSrv.getShopParams();
  }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopSrv.getProducts().subscribe({
      next: response => {
        this.products = response.data;
        this.totalCount = response.count;
      },
      error: errorMsg => console.log(errorMsg)
    });
  }

  getBrands() {
    this.shopSrv.getBrands().subscribe({
      next: response => this.brands = [{id: 0, name: 'All'}, ...response],
      error: errorMsg => console.log(errorMsg)
    });
  }

  getTypes() {
    this.shopSrv.getTypes().subscribe({
      next: response => this.types = [{id: 0, name: 'All'}, ...response],
      error: errorMsg => console.log(errorMsg)
    });
  }

  onBrandSelected(brandId: number) {
    const params = this.shopSrv.getShopParams();
    params.brandId = brandId;
    params.pageNumber = 1;
    this.shopSrv.setShopParams(params);
    this.shopParams = params;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    const params = this.shopSrv.getShopParams();
    params.typeId = typeId;
    params.pageNumber = 1;
    this.shopSrv.setShopParams(params);
    this.shopParams = params;
    this.getProducts();
  }

  onSortSelected(event: any) {
    const params = this.shopSrv.getShopParams();
    params.sort = event.target.value;
    this.shopSrv.setShopParams(params);
    this.shopParams = params;
    this.getProducts();
  }

  onPageChanged(event: any) {
    const params = this.shopSrv.getShopParams();
    if (params.pageNumber != event.page) {
      params.pageNumber = event.page;
      this.shopSrv.setShopParams(params);
      this.shopParams = params;
      this.getProducts();
    }
  }

  onSearch(){
    const params = this.shopSrv.getShopParams();
    params.search = this.searchInput?.nativeElement.value;
    params.pageNumber = 1;
    this.shopSrv.setShopParams(params);
    this.shopParams = params;
    this.getProducts();
  }

  onReset() {
    if (this.searchInput) this.searchInput.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.shopSrv.setShopParams(this.shopParams);
    this.getProducts();
  }
}
