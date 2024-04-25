import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewDoctorsListComponent } from './view-doctors-list.component';

describe('ViewDoctorsListComponent', () => {
  let component: ViewDoctorsListComponent;
  let fixture: ComponentFixture<ViewDoctorsListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ViewDoctorsListComponent]
    });
    fixture = TestBed.createComponent(ViewDoctorsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
