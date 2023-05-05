import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {DeliveryMethod} from "../shared/models/deliveryMethod.model";
import {map} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  constructor(private http: HttpClient) { }

  getDeliveryMethods() {
    return this.http.get<DeliveryMethod[]>('orders/deliveryMethods').pipe(
      map(dm => {
        return dm.sort((a, b) => b.price - a.price)
      })
    );
  }
}
