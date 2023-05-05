import { Component } from '@angular/core';
import {BasketService} from "./basket.service";
import {BasketItem} from "../shared/models/basketItem.model";

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html'
})
export class BasketComponent {
  constructor(public basketSrv: BasketService) { }

  incrementQuantity(item: BasketItem) {
    this.basketSrv.addItemToBasket(item);
  }

  removeItem(event: {id: number, quantity: number}) {
    this.basketSrv.removeItemFromBasket(event.id, event.quantity);
  }
}
