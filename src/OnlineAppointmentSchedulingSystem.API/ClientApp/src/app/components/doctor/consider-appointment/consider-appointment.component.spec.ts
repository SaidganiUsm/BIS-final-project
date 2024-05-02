import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsiderAppointmentComponent } from './consider-appointment.component';

describe('ConsiderAppointmentComponent', () => {
  let component: ConsiderAppointmentComponent;
  let fixture: ComponentFixture<ConsiderAppointmentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConsiderAppointmentComponent]
    });
    fixture = TestBed.createComponent(ConsiderAppointmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
