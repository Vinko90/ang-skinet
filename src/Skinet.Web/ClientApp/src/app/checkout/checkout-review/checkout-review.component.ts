import {Component, Input} from '@angular/core';
import {BasketService} from "../../basket/basket.service";
import {ToastrService} from "ngx-toastr";
import {CdkStepper} from "@angular/cdk/stepper";

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html'
})
export class CheckoutReviewComponent {
  @Input() appStepper?: CdkStepper;

  constructor(private basketSrv: BasketService, private toast: ToastrService) { }

  createIntent() {
    this.basketSrv.createPaymentIntent().subscribe({
      next: () => {
        this.toast.success('Payment intent created');
        this.appStepper?.next();
      },
      error: err => this.toast.error(err.message)
    });
  }
}
