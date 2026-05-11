import { CommonModule, CurrencyPipe, NgIf } from '@angular/common';
import { Component, Input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-result-card',
  standalone: true,
  imports: [CommonModule, CurrencyPipe, MatIconModule, NgIf],
  templateUrl: './result-card.component.html',
  styleUrl: './result-card.component.scss',
})
export class ResultCardComponent {
  @Input({ required: true }) label = '';
  @Input({ required: true }) value = 0;
  @Input() icon?: string;
  @Input() tone: 'default' | 'profit' | 'tax' = 'default';
}
