import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustByBillInfoComponent } from './cust-by-bill-info.component';

describe('CustByBillInfoComponent', () => {
  let component: CustByBillInfoComponent;
  let fixture: ComponentFixture<CustByBillInfoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustByBillInfoComponent]
    });
    fixture = TestBed.createComponent(CustByBillInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
