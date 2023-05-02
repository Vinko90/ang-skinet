import {Inject, Injectable} from "@angular/core";
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable()
export class BaseurlInterceptorService implements HttpInterceptor {
  constructor(@Inject('BASE_URL') private baseUrl: string) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const apiReq = request.clone({ url: `${this.baseUrl}api/${request.url}` });
    return next.handle(apiReq);
  }
}
