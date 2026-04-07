import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPremissions } from './view-premissions';

describe('ViewPremissions', () => {
  let component: ViewPremissions;
  let fixture: ComponentFixture<ViewPremissions>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewPremissions],
    }).compileComponents();

    fixture = TestBed.createComponent(ViewPremissions);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
