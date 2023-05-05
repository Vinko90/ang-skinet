import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import {Observable, take} from 'rxjs';
import {AccountService} from "../../account/account.service";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  token?: string;

  constructor(private accSrv: AccountService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.accSrv.currentUserSource$.pipe(take(1)).subscribe({
      next: userToken => this.token = userToken?.token
    });

    if (this.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.token}`
        }
      });
    }

    return next.handle(request);
  }
}
