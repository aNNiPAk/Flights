/* tslint:disable */
/* eslint-disable */
import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';

import { FlightRm } from '../models/flight-rm';

@Injectable({ providedIn: 'root' })
export class FlightService extends BaseService {
  constructor(config: ApiConfiguration, http: HttpClient) {
    super(config, http);
  }

  /** Path part for operation `flightGet()` */
  static readonly FlightGetPath = '/Flight';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `flightGet$Plain()` instead.
   *
   * This method doesn't expect any request body.
   */
  flightGet$Plain$Response(
    params?: {
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<FlightRm>>> {
    const rb = new RequestBuilder(this.rootUrl, FlightService.FlightGetPath, 'get');
    if (params) {
    }

    return this.http.request(
      rb.build({ responseType: 'text', accept: 'text/plain', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<FlightRm>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `flightGet$Plain$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  flightGet$Plain(
    params?: {
    },
    context?: HttpContext
  ): Observable<Array<FlightRm>> {
    return this.flightGet$Plain$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<FlightRm>>): Array<FlightRm> => r.body)
    );
  }

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `flightGet()` instead.
   *
   * This method doesn't expect any request body.
   */
  flightGet$Response(
    params?: {
    },
    context?: HttpContext
  ): Observable<StrictHttpResponse<Array<FlightRm>>> {
    const rb = new RequestBuilder(this.rootUrl, FlightService.FlightGetPath, 'get');
    if (params) {
    }

    return this.http.request(
      rb.build({ responseType: 'json', accept: 'text/json', context })
    ).pipe(
      filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<FlightRm>>;
      })
    );
  }

  /**
   * This method provides access only to the response body.
   * To access the full response (for headers, for example), `flightGet$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  flightGet(
    params?: {
    },
    context?: HttpContext
  ): Observable<Array<FlightRm>> {
    return this.flightGet$Response(params, context).pipe(
      map((r: StrictHttpResponse<Array<FlightRm>>): Array<FlightRm> => r.body)
    );
  }

}
