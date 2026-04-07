import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManagePremission } from './manage-premission';

describe('ManagePremission', () => {
  let component: ManagePremission;
  let fixture: ComponentFixture<ManagePremission>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ManagePremission],
    }).compileComponents();

    fixture = TestBed.createComponent(ManagePremission);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
