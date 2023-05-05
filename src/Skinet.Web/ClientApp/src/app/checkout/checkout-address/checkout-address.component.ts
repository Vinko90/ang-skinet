import {Component, Input} from '@angular/core';
import {FormGroup} from "@angular/forms";
import {AccountService} from "../../account/account.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html'
})
export class CheckoutAddressComponent {
  @Input() addressForm?: FormGroup;

  constructor(private accSrv: AccountService, private toastr: ToastrService) { }

  saveUserAddress() {
    this.accSrv.updateUserAddress(this.addressForm?.get('addressForm')?.value).subscribe({
      next: () => {
        this.toastr.success('Address Saved')
        this.addressForm?.get('addressForm')?.reset(this.addressForm?.get('addressForm')?.value);
      }
    });
  }
}
