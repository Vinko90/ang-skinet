import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HTTP_INTERCEPTORS} from "@angular/common/http";
import { PaginationModule } from 'ngx-bootstrap/pagination';

import {BaseurlInterceptorService} from "./interceptors/baseurl-interceptor.service";
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import {ErrorInterceptor} from "./interceptors/error.interceptor";

@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: BaseurlInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent
  ]
})
export class SharedModule { }
