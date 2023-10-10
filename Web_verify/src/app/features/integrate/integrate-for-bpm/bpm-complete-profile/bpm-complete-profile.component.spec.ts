import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BpmCompleteProfileComponent } from './bpm-complete-profile.component';

describe('BpmCompleteProfileComponent', () => {
  let component: BpmCompleteProfileComponent;
  let fixture: ComponentFixture<BpmCompleteProfileComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BpmCompleteProfileComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BpmCompleteProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
