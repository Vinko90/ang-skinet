import {Component, Input, OnInit} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {DeliveryMethod} from "../../shared/models/deliveryMethod.model";
import {CheckoutService} from "../checkout.service";
import {BasketService} from "../../basket/basket.service";

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html'
})
export class CheckoutDeliveryComponent implements OnInit {
  @Input() checkoutForm?: FormGroup;
  deliveryMethods: DeliveryMethod[] = [];

  constructor(private checkoutSrv: CheckoutService, private basketSrv: BasketService) { }

  ngOnInit(): void {
    this.checkoutSrv.getDeliveryMethods().subscribe({
      next: dm => this.deliveryMethods = dm
    });
  }

  setShippingPrice(deliveryMethod: DeliveryMethod) {
    this.basketSrv.setShippingPrice(deliveryMethod);
  }
}
