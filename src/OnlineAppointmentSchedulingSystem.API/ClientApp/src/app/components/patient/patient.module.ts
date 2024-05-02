import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MatButtonModule } from '@angular/material/button';
import { PatientRoutingModule } from './patient-routing.module';

@NgModule({
    declarations: [DashboardComponent],
    imports: [CommonModule, MatButtonModule, PatientRoutingModule],
})
export class PatientModule {}
