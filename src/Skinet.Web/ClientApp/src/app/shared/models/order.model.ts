import {Address} from "./address.model";
import {OrderItem} from "./orderItem.model";

export interface Order {
  id: number
  buyerEmail: string
  orderDate: string
  shipToAddress: Address
  deliveryMethod: string
  shippingPrice: number
  orderItems: OrderItem[]
  subtotal: number
  total: number
  status: string
}
