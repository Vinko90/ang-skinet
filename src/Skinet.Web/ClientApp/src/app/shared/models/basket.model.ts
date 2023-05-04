import {BasketItem} from "./basketItem.model";
import {createId} from "@paralleldrive/cuid2";

export interface Basket {
  id: string;
  items: BasketItem[];
}

export class Basket implements Basket {
  id = createId();
  items: BasketItem[] = [];
}

export interface BasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
