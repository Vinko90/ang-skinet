import {Component, OnInit} from '@angular/core';
import {BasketService} from "./basket/basket.service";
import {AccountService} from "./account/account.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(private basketSrv: BasketService, private accSrv: AccountService) { }

  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    this.accSrv.loadCurrentUser(token).subscribe();
  }

  loadBasket() {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) this.basketSrv.getBasket(basketId);
  }
}
