import { AsyncPipe } from '@angular/common';
import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { BehaviorSubject, finalize } from 'rxjs';
import { CalculatorFormComponent } from '../components/calculator-form/calculator-form.component';
import { InvestmentResultComponent } from '../components/investment-result/investment-result.component';
import { CdbCalculationRequest } from '../models/cdb-calculation-request.model';
import { CdbCalculationResult } from '../models/cdb-calculation-result.model';
import { CdbCalculatorApiService } from '../services/cdb-calculator-api.service';

@Component({
  selector: 'app-cdb-calculator-page',
  standalone: true,
  imports: [AsyncPipe, CalculatorFormComponent, InvestmentResultComponent, MatIconModule],
  templateUrl: './cdb-calculator-page.component.html',
  styleUrl: './cdb-calculator-page.component.scss',
})
export class CdbCalculatorPageComponent {
  private readonly isLoadingSubject = new BehaviorSubject(false);

  readonly isLoading$ = this.isLoadingSubject.asObservable();
  result: CdbCalculationResult | null = null;
  errorMessage: string | null = null;

  constructor(private readonly apiService: CdbCalculatorApiService) {}

  calculate(request: CdbCalculationRequest): void {
    this.errorMessage = null;
    this.isLoadingSubject.next(true);

    this.apiService
      .calculate(request)
      .pipe(finalize(() => this.isLoadingSubject.next(false)))
      .subscribe({
        next: (result) => {
          this.result = result;
        },
        error: (error: Error) => {
          this.errorMessage = error.message;
        },
      });
  }
}
