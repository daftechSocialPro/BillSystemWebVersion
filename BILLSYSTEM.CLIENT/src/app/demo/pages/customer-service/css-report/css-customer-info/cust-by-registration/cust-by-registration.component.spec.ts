import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustByRegistrationComponent } from './cust-by-registration.component';

describe('CustByRegistrationComponent', () => {
  let component: CustByRegistrationComponent;
  let fixture: ComponentFixture<CustByRegistrationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustByRegistrationComponent]
    });
    fixture = TestBed.createComponent(CustByRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
