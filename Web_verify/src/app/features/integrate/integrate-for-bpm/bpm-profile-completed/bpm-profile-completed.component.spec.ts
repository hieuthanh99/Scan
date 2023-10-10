import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BpmProfileCompletedComponent } from './bpm-profile-completed.component';

describe('BpmProfileCompletedComponent', () => {
  let component: BpmProfileCompletedComponent;
  let fixture: ComponentFixture<BpmProfileCompletedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BpmProfileCompletedComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BpmProfileCompletedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
