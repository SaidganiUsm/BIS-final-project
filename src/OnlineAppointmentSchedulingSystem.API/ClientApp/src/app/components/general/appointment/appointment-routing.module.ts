import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentsPageComponent } from './appointments-page/appointments-page.component';
import { DoctorsListPageComponent } from './doctors-list-page/doctors-list-page.component';
import { DoctorAppointmentsComponent } from './doctor-appointments/doctor-appointments.component';

const routes: Routes = [
    {
        path: 'calendar',
        component: AppointmentsPageComponent,
        pathMatch: 'full',
    },
    {
        path: 'doctors',
        component: DoctorsListPageComponent,
        pathMatch: 'full',
    },
    {
        path: 'doctor/:id',
        component: DoctorAppointmentsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppointmentRoutingModule {}
