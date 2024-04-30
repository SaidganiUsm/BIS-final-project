import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DialogPopupComponent } from './dialog-popup/dialog-popup.component';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { ChoicePopupComponent } from './choice-popup/choice-popup.component';
import { CalendarComponent } from './calendar/calendar.component';
import { MiniProfileBarComponent } from './mini-profile-bar/mini-profile-bar.component';
import { StaffCalendarComponent } from './staff-calendar/staff-calendar.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { DoctorCalendarComponent } from './doctor-calendar/doctor-calendar.component';

@NgModule({
    declarations: [
        DialogPopupComponent,
        ChoicePopupComponent,
        CalendarComponent,
        MiniProfileBarComponent,
        StaffCalendarComponent,
        DoctorCalendarComponent,
    ],
    imports: [
        CommonModule,
        FormsModule,
        MatButtonModule,
        RouterModule,
        MatIconModule,
        MatSnackBarModule,
    ],
    exports: [
        MiniProfileBarComponent,
        CalendarComponent,
        StaffCalendarComponent,
        DoctorCalendarComponent,
    ],
})
export class UiElementsModule {}
