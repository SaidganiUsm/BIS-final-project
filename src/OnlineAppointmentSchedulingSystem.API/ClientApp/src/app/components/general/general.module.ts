import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainPageComponent } from './main-page/main-page.component';
import { AppointmentModule } from './appointment/appointment.module';

@NgModule({
    declarations: [MainPageComponent],
    imports: [CommonModule, AppointmentModule],
})
export class GeneralModule {}
