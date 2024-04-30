import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from './components/general/main-page/main-page.component';
import { isDoctorGuard } from './guards/doctor/is-doctor.guard';
import { isPatientGuard } from './guards/patient/is-patient.guard';
import { isStaffGuard } from './guards/staff/is-staff.guard';
import { isLoggedInGuard } from './guards/is-logged-in.guard';

const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: MainPageComponent },
    {
        path: 'profile',
        loadChildren: () =>
            import('./components/general/profile/profile.module').then(
                (m) => m.ProfileModule
            ),
        data: { breadcrumb: { skip: true } },
        canActivate: [isLoggedInGuard],
    },
    {
        path: 'appointment',
        loadChildren: () =>
            import('./components/general/appointment/appointment.module').then(
                (m) => m.AppointmentModule
            ),
        data: { breadcrumb: { skip: true } },
    },
    {
        path: 'patient',
        loadChildren: () =>
            import('./components/patient/patient.module').then(
                (m) => m.PatientModule
            ),
        data: { breadcrumb: { skip: true } },
        canActivate: [isPatientGuard],
    },
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes, { scrollPositionRestoration: 'enabled' }),
    ],
    exports: [RouterModule],
})
export class AppRoutingModule {}
