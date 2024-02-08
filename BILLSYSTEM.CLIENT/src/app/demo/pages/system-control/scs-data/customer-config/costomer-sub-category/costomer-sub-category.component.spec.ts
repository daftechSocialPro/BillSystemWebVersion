import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CostomerSubCategoryComponent } from './costomer-sub-category.component';

describe('CostomerSubCategoryComponent', () => {
  let component: CostomerSubCategoryComponent;
  let fixture: ComponentFixture<CostomerSubCategoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CostomerSubCategoryComponent]
    });
    fixture = TestBed.createComponent(CostomerSubCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
