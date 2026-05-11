import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { of, throwError } from 'rxjs';
import { CdbCalculatorApiService } from '../services/cdb-calculator-api.service';
import { CdbCalculatorPageComponent } from './cdb-calculator-page.component';

describe('CdbCalculatorPageComponent', () => {
  let component: CdbCalculatorPageComponent;
  let fixture: ComponentFixture<CdbCalculatorPageComponent>;
  let apiService: jasmine.SpyObj<CdbCalculatorApiService>;

  beforeEach(async () => {
    apiService = jasmine.createSpyObj<CdbCalculatorApiService>('CdbCalculatorApiService', ['calculate']);

    await TestBed.configureTestingModule({
      imports: [CdbCalculatorPageComponent, NoopAnimationsModule],
      providers: [{ provide: CdbCalculatorApiService, useValue: apiService }],
    }).compileComponents();

    fixture = TestBed.createComponent(CdbCalculatorPageComponent);
    component = fixture.componentInstance;
  });

  it('should store result after successful calculation', () => {
    const result = {
      grossAmount: 1123.08,
      netAmount: 1098.47,
      taxAmount: 24.62,
      grossProfit: 123.08,
    };
    apiService.calculate.and.returnValue(of(result));

    component.calculate({ initialAmount: 1000, months: 12 });

    expect(component.result).toEqual(result);
    expect(component.errorMessage).toBeNull();
  });

  it('should expose friendly error message when API fails', () => {
    apiService.calculate.and.returnValue(throwError(() => new Error('API indisponivel.')));

    component.calculate({ initialAmount: 1000, months: 12 });

    expect(component.result).toBeNull();
    expect(component.errorMessage).toBe('API indisponivel.');
  });
});
