import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewDoctorsListComponent } from './view-doctors-list/view-doctors-list.component';
import { CreateAppointmentComponent } from './create-appointment/create-appointment.component';

@NgModule({
    declarations: [CreateAppointmentComponent, ViewDoctorsListComponent],
    imports: [CommonModule],
})
export class PatientModule {}
