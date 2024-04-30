import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorsListPageComponent } from './doctors-list-page.component';

describe('DoctorsListPageComponent', () => {
  let component: DoctorsListPageComponent;
  let fixture: ComponentFixture<DoctorsListPageComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DoctorsListPageComponent]
    });
    fixture = TestBed.createComponent(DoctorsListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
