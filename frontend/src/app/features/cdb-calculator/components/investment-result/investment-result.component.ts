import { Component, Input } from '@angular/core';
import { ResultCardComponent } from '../../../../shared/components/result-card/result-card.component';
import { CdbCalculationResult } from '../../models/cdb-calculation-result.model';

@Component({
  selector: 'app-investment-result',
  standalone: true,
  imports: [ResultCardComponent],
  templateUrl: './investment-result.component.html',
  styleUrl: './investment-result.component.scss',
})
export class InvestmentResultComponent {
  @Input({ required: true }) result!: CdbCalculationResult;
}
