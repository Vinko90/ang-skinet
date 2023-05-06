import { Component } from '@angular/core';
import {BasketService} from "../../basket/basket.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html'
})
export class CheckoutReviewComponent {
  constructor(private basketSrv: BasketService, private toast: ToastrService) {
  }

  createIntent() {
    this.basketSrv.createPaymentIntent().subscribe({
      next: () => this.toast.success('Payment intent created'),
      error: err => this.toast.error(err.message)
    });
  }
}
