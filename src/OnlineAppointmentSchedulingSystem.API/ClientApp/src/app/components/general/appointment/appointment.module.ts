import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppointmentRoutingModule } from './appointment-routing.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { AppointmentsPageComponent } from './appointments-page/appointments-page.component';
import { UiElementsModule } from 'src/app/ui-elements/ui-elements.module';

@NgModule({
    declarations: [AppointmentsPageComponent],
    imports: [
        CommonModule,
        AppointmentRoutingModule,
        MatIconModule,
        MatButtonModule,
        UiElementsModule
    ],
})
export class AppointmentModule {}
