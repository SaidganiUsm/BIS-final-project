import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentRoutingModule } from './appointment-routing.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AppointmentsPageComponent } from './appointments-page/appointments-page.component';
import { UiElementsModule } from 'src/app/ui-elements/ui-elements.module';
import { DoctorsListPageComponent } from './doctors-list-page/doctors-list-page.component';
import { CreateAppointmentComponent } from './create-appointment/create-appointment.component';
import { DoctorAppointmentsComponent } from './doctor-appointments/doctor-appointments.component';

@NgModule({
    declarations: [
        AppointmentsPageComponent,
        DoctorsListPageComponent,
        CreateAppointmentComponent,
        DoctorAppointmentsComponent,
    ],
    imports: [
        CommonModule,
        AppointmentRoutingModule,
        MatIconModule,
        MatButtonModule,
        UiElementsModule,
    ],
})
export class AppointmentModule {}
