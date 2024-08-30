import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisconnectCustComponent } from './disconnect-cust.component';

describe('DisconnectCustComponent', () => {
  let component: DisconnectCustComponent;
  let fixture: ComponentFixture<DisconnectCustComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DisconnectCustComponent]
    });
    fixture = TestBed.createComponent(DisconnectCustComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
