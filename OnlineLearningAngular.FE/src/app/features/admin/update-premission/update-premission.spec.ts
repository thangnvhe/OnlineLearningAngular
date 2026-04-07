import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdatePremission } from './update-premission';

describe('UpdatePremission', () => {
  let component: UpdatePremission;
  let fixture: ComponentFixture<UpdatePremission>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [UpdatePremission],
    }).compileComponents();

    fixture = TestBed.createComponent(UpdatePremission);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
