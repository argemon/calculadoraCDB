import { TestBed } from '@angular/core/testing';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { environment } from '../../../../environments/environment';
import { CdbCalculatorApiService } from './cdb-calculator-api.service';

describe('CdbCalculatorApiService', () => {
  let service: CdbCalculatorApiService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });

    service = TestBed.inject(CdbCalculatorApiService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should post calculation request to API', () => {
    const expectedResult = {
      grossAmount: 1123.08,
      netAmount: 1098.47,
      taxAmount: 24.62,
      grossProfit: 123.08,
    };

    service.calculate({ initialAmount: 1000, months: 12 }).subscribe((result) => {
      expect(result).toEqual(expectedResult);
    });

    const request = httpMock.expectOne(`${environment.apiBaseUrl}/cdb-calculator/calculate`);
    expect(request.request.method).toBe('POST');
    expect(request.request.body).toEqual({ initialAmount: 1000, months: 12 });
    request.flush(expectedResult);
  });
});
