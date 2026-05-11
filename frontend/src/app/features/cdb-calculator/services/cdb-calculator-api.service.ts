import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment';
import { CdbCalculationRequest } from '../models/cdb-calculation-request.model';
import { CdbCalculationResult } from '../models/cdb-calculation-result.model';

@Injectable({ providedIn: 'root' })
export class CdbCalculatorApiService {
  private readonly endpoint = `${environment.apiBaseUrl}/cdb-calculator/calculate`;

  constructor(private readonly httpClient: HttpClient) {}

  calculate(request: CdbCalculationRequest): Observable<CdbCalculationResult> {
    return this.httpClient.post<CdbCalculationResult>(this.endpoint, request);
  }
}
