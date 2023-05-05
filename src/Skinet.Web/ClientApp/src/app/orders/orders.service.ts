import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Order} from "../shared/models/order.model";

@Injectable({
  providedIn: 'root'
})
export class OrdersService {
  constructor(private http: HttpClient) { }

  getOrdersForUser() {
    return this.http.get<Order[]>('orders');
  }

  getOrderDetailed(id: number) {
    return this.http.get<Order>('orders/' + id);
  }
}
