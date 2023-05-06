import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {BasketService} from "../../basket/basket.service";
import {CheckoutService} from "../checkout.service";
import {ToastrService} from "ngx-toastr";
import {Basket} from "../../shared/models/basket.model";
import {Address} from "../../shared/models/address.model";
import {NavigationExtras, Router} from "@angular/router";
import {
  loadStripe,
  Stripe,
  StripeCardCvcElement,
  StripeCardExpiryElement,
  StripeCardNumberElement
} from "@stripe/stripe-js";
import {environment} from "../../../environments/environment";
import {firstValueFrom} from "rxjs";
import {OrderToCreate} from "../../shared/models/orderToCreate.model";


@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html'
})
export class CheckoutPaymentComponent implements OnInit {
  @Input() checkoutForm?: FormGroup;
  @ViewChild('cardNumber') cardNumberElement?: ElementRef;
  @ViewChild('cardExpiry') cardExpiryElement?: ElementRef;
  @ViewChild('cardCvc') cardCvcElement?: ElementRef;
  stripe: Stripe | null = null;
  cardNumber?: StripeCardNumberElement;
  cardExpiry?: StripeCardExpiryElement;
  cardCvc?: StripeCardCvcElement;
  cardErrors: any;
  loading = false;

  constructor(private basketSrv: BasketService,
              private checkoutSrv: CheckoutService,
              private toastr: ToastrService,
              private router: Router) { }

  ngOnInit(): void {
    loadStripe(environment.stripePubKey).then(stripe => {
      this.stripe = stripe;
      const elements = stripe?.elements();
      if (elements) {
        this.cardNumber = elements.create('cardNumber');
        this.cardNumber.mount(this.cardNumberElement?.nativeElement);
        this.cardNumber.on('change', event => {
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardExpiry = elements.create('cardExpiry');
        this.cardExpiry.mount(this.cardExpiryElement?.nativeElement);
        this.cardExpiry.on('change', event => {
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })

        this.cardCvc = elements.create('cardCvc');
        this.cardCvc.mount(this.cardCvcElement?.nativeElement);
        this.cardCvc.on('change', event => {
          if (event.error) this.cardErrors = event.error.message;
          else this.cardErrors = null;
        })
      }
    });
  }

  async submitOrder() {
    this.loading = true;
    const basket = this.basketSrv.getCurrentBasketValue();

    try {
      const createdOrder = await this.createOrder(basket);
      const paymentResult = await this.confirmPaymentWithStripe(basket);

      if (paymentResult.paymentIntent) {
        this.basketSrv.deleteLocalBasket();
        const navExtra: NavigationExtras = {state: createdOrder};
        this.router.navigate(['checkout/success'], navExtra);
      } else {
        this.toastr.error(paymentResult.error.message);
      }

    } catch (error: any) {
      console.log(error);
      this.toastr.error(error.message);
    } finally {
      this.loading = false;
    }
  }

  private async createOrder(basket: Basket | null) {
    if (!basket) throw new Error('createOrder() -> Basket is null');
    const orderToCreate = this.getOrderToCreate(basket);
    return firstValueFrom(this.checkoutSrv.createOrder(orderToCreate));
  }

  private async confirmPaymentWithStripe(basket: Basket | null) {
    if (!basket) throw new Error('confirmPaymentWithStripe() -> Basket is null');
    const result = this.stripe?.confirmCardPayment(basket.clientSecret!, {
      payment_method: {
        card: this.cardNumber!,
        billing_details: {
          name: this.checkoutForm?.get('paymentForm')?.get('nameOnCard')?.value
        }
      }
    });
    if (!result) throw new Error('confirmPaymentWithStripe() -> Problem attempting payment with stripe');
    return result;
  }

  private getOrderToCreate(basket: Basket): OrderToCreate {
    const deliveryMethodId = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
    const shipToAddress = this.checkoutForm?.get('addressForm')?.value as Address;
    if (!deliveryMethodId || !shipToAddress) throw new Error('Problem in getOrderToCreate()');
    return {
      basketId: basket.id,
      deliveryMethodId: deliveryMethodId,
      shipToAddress: shipToAddress
    }
  }
}
