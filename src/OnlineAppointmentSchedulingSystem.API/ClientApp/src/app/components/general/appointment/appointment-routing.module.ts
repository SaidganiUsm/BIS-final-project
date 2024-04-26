import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppointmentsPageComponent } from './appointments-page/appointments-page.component';

const routes: Routes = [
    {
        path: 'calendar',
        component: AppointmentsPageComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class AppointmentRoutingModule {}
