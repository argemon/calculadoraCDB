import { Routes } from '@angular/router';
import { CdbCalculatorPageComponent } from './features/cdb-calculator/pages/cdb-calculator-page.component';

export const routes: Routes = [
  {
    path: '',
    component: CdbCalculatorPageComponent,
    title: 'Calculadora CDB',
  },
];
