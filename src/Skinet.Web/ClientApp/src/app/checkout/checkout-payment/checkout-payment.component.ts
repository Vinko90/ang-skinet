import {Component, Input} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {BasketService} from "../../basket/basket.service";
import {CheckoutService} from "../checkout.service";
import {ToastrService} from "ngx-toastr";
import {Basket} from "../../shared/models/basket.model";
import {Address} from "../../shared/models/address.model";
import {NavigationExtras, Router} from "@angular/router";

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html'
})
export class CheckoutPaymentComponent {
  @Input() checkoutForm?: FormGroup;

  constructor(private basketSrv: BasketService,
              private checkoutSrv: CheckoutService,
              private toastr: ToastrService,
              private router: Router) { }

  submitOrder() {
    const basket = this.basketSrv.getCurrentBasketValue();
    if (!basket) return;
    const orderToCreate = this.getOrderToCreate(basket);
    if (!orderToCreate) return;
    this.checkoutSrv.createOrder(orderToCreate).subscribe({
      next: order => {
        this.toastr.success('Order created successfully');
        this.basketSrv.deleteLocalBasket();
        const navExtra: NavigationExtras = {state: order};
        this.router.navigate(['checkout/success'], navExtra);
      }
    });
  }

  private getOrderToCreate(basket: Basket) {
    const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
    const shipToAddress = this.checkoutForm?.get('addressForm')?.value as Address;
    if (!deliveryMethodId || !shipToAddress) return;
    return {
      basketId: basket.id,
      deliveryMethodId: deliveryMethodId,
      shipToAddress: shipToAddress
    }
  }
}
