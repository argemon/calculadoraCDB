import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { CalculatorFormComponent } from './calculator-form.component';

describe('CalculatorFormComponent', () => {
  let component: CalculatorFormComponent;
  let fixture: ComponentFixture<CalculatorFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CalculatorFormComponent, NoopAnimationsModule],
    }).compileComponents();

    fixture = TestBed.createComponent(CalculatorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should emit request when form is valid', () => {
    spyOn(component.calculate, 'emit');
    component.form.setValue({ initialAmount: 1000, months: 12 });

    component.submit();

    expect(component.calculate.emit).toHaveBeenCalledOnceWith({ initialAmount: 1000, months: 12 });
  });

  it('should not emit when form is invalid', () => {
    spyOn(component.calculate, 'emit');
    component.form.setValue({ initialAmount: 0, months: 1 });

    component.submit();

    expect(component.calculate.emit).not.toHaveBeenCalled();
    expect(component.form.invalid).toBeTrue();
  });
});
