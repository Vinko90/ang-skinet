import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import { PaginationModule } from 'ngx-bootstrap/pagination';

import {BaseurlInterceptor} from "./interceptors/baseurl.interceptor";
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import {ErrorInterceptor} from "./interceptors/error.interceptor";
import {LoadingInterceptor} from "./interceptors/loading.interceptor";
import {CarouselModule} from "ngx-bootstrap/carousel";
import { OrderTotalsComponent } from './order-totals/order-totals.component';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {BsDropdownModule} from "ngx-bootstrap/dropdown";
import { TextInputComponent } from './text-input/text-input.component';
import { StepperComponent } from './stepper/stepper.component';
import {CdkStepperModule} from "@angular/cdk/stepper";
import {JwtInterceptor} from "./interceptors/jwt.interceptor";
import { BasketSummaryComponent } from './basket-summary/basket-summary.component';
import {RouterLink} from "@angular/router";

@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    OrderTotalsComponent,
    TextInputComponent,
    StepperComponent,
    BasketSummaryComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot(),
    BsDropdownModule.forRoot(),
    CdkStepperModule,
    RouterLink
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseurlInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true
    }
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    BsDropdownModule,
    OrderTotalsComponent,
    ReactiveFormsModule,
    TextInputComponent,
    StepperComponent,
    CdkStepperModule,
    BasketSummaryComponent
  ]
})
export class SharedModule { }
