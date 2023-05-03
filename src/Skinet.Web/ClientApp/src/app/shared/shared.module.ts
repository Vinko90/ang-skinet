import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HTTP_INTERCEPTORS} from "@angular/common/http";

import {BaseurlInterceptorService} from "./interceptors/baseurl-interceptor.service";

@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseurlInterceptorService,
      multi: true
    }
  ]
})
export class SharedModule { }
