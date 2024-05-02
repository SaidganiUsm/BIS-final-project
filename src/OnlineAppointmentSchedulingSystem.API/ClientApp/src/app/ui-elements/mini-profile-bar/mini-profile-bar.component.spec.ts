import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MiniProfileBarComponent } from './mini-profile-bar.component';

describe('MiniProfileBarComponent', () => {
  let component: MiniProfileBarComponent;
  let fixture: ComponentFixture<MiniProfileBarComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MiniProfileBarComponent]
    });
    fixture = TestBed.createComponent(MiniProfileBarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
