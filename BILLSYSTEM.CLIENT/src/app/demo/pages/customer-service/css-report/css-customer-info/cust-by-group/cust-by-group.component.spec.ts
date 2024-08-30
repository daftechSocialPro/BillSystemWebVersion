import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustByGroupComponent } from './cust-by-group.component';

describe('CustByGroupComponent', () => {
  let component: CustByGroupComponent;
  let fixture: ComponentFixture<CustByGroupComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CustByGroupComponent]
    });
    fixture = TestBed.createComponent(CustByGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
