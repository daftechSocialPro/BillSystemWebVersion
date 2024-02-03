import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CssBatchRecordsComponent } from './css-batch-records.component';

describe('CssBatchRecordsComponent', () => {
  let component: CssBatchRecordsComponent;
  let fixture: ComponentFixture<CssBatchRecordsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CssBatchRecordsComponent]
    });
    fixture = TestBed.createComponent(CssBatchRecordsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
