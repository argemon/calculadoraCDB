import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CdbCalculationRequest } from '../../models/cdb-calculation-request.model';

@Component({
  selector: 'app-calculator-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatProgressSpinnerModule,
  ],
  templateUrl: './calculator-form.component.html',
  styleUrl: './calculator-form.component.scss',
})
export class CalculatorFormComponent {
  private readonly formBuilder = inject(FormBuilder);

  @Input() isLoading = false;
  @Output() calculate = new EventEmitter<CdbCalculationRequest>();

  readonly form = this.formBuilder.nonNullable.group({
    initialAmount: [1000, [Validators.required, Validators.min(0.01)]],
    months: [12, [Validators.required, Validators.min(2)]],
  });

  submit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.calculate.emit(this.form.getRawValue());
  }

  get initialAmountMessage(): string {
    const control = this.form.controls.initialAmount;
    return control.hasError('required')
      ? 'Informe o valor inicial.'
      : 'O valor precisa ser maior que zero.';
  }

  get monthsMessage(): string {
    const control = this.form.controls.months;
    return control.hasError('required') ? 'Informe o prazo.' : 'O prazo precisa ser maior que 1 mes.';
  }
}
