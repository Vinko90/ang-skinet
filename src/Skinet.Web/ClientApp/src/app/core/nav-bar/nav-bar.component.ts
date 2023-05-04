import { Component } from '@angular/core';
import {BasketService} from "../../basket/basket.service";
import {BasketItem} from "../../shared/models/basketItem.model";
import {AccountService} from "../../account/account.service";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent {
  constructor(public basketSrv: BasketService, public accService: AccountService) { }

  getCount(items: BasketItem[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0);
  }
}
