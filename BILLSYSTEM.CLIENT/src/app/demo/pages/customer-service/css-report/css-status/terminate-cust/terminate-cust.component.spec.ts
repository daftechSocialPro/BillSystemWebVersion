import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TerminateCustComponent } from './terminate-cust.component';

describe('TerminateCustComponent', () => {
  let component: TerminateCustComponent;
  let fixture: ComponentFixture<TerminateCustComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TerminateCustComponent]
    });
    fixture = TestBed.createComponent(TerminateCustComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
