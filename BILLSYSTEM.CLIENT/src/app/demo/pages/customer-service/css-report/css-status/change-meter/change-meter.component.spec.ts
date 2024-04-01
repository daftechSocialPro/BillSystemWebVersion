import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChangeMeterComponent } from './change-meter.component';

describe('ChangeMeterComponent', () => {
  let component: ChangeMeterComponent;
  let fixture: ComponentFixture<ChangeMeterComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ChangeMeterComponent]
    });
    fixture = TestBed.createComponent(ChangeMeterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
