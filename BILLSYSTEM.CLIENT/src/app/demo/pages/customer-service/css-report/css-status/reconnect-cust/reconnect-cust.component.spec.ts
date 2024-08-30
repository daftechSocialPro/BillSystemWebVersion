import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReconnectCustComponent } from './reconnect-cust.component';

describe('ReconnectCustComponent', () => {
  let component: ReconnectCustComponent;
  let fixture: ComponentFixture<ReconnectCustComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReconnectCustComponent]
    });
    fixture = TestBed.createComponent(ReconnectCustComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
