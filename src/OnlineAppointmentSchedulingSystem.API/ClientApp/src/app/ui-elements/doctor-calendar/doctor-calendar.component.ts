import { Dialog } from '@angular/cdk/dialog';
import { Component, Input } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthorizeService } from 'src/app/api-authorization/authorize.service';
import {
    AppointmentDto,
    AppointmentDtoForDoctor,
} from 'src/app/models/Appointments/appointment-model';
import { Client } from 'src/app/web-api-client';
import { ChoicePopupComponent } from '../choice-popup/choice-popup.component';

@Component({
    selector: 'app-doctor-calendar',
    templateUrl: './doctor-calendar.component.html',
    styleUrls: ['./doctor-calendar.component.scss'],
})
export class DoctorCalendarComponent {
    @Input() userAppointments: AppointmentDtoForDoctor[] = [];
    @Input() doctorId!: number;
    @Input() date!: Date;

    isUserDoctor: boolean = false;
    isUserPatient: boolean = false;

    errorMessage!: string;

    constructor(
        private dialog: Dialog,
        private client: Client,
        private snackBar: MatSnackBar,
        private authService: AuthorizeService
    ) {}

    getAppointmentsForTimeSlot(
        hour: number,
        minute: string
    ): AppointmentDtoForDoctor[] {
        return this.userAppointments.filter((appointment) => {
            const appointmentTime = new Date(appointment.date);
            const appointmentHour = appointmentTime.getHours();
            const appointmentMinute = appointmentTime.getMinutes();
            return (
                appointmentHour === hour &&
                Math.floor(appointmentMinute / 10) * 10 === parseInt(minute)
            );
        });
    }

    getColorForStatus(hour: number, minute: string): string {
        const appointments = this.getAppointmentsForTimeSlot(hour, minute);
        if (appointments.length > 0) {
            const status = appointments[0].appointmentStatus.name;

            switch (status) {
                case 'Done':
                    return 'green';
                case 'Approved':
                    return '#2b5293';
                case 'PendingApproval':
                    return 'grey';
                case 'Cancelled':
                case 'Rejected':
                    return '#c30010';
                default:
                    return 'black';
            }
        } else {
            return 'white';
        }
    }

    hours: number[] = Array.from({ length: 11 }, (_, i) => i + 8);
    minutes: string[] = Array.from({ length: 6 }, (_, i) =>
        (i * 10).toString()
    );

    handleTimeSlotClick(hour: number, minute: string) {
        this.handleConsiderAppointment(hour, minute);
    }

    handleConsiderAppointment(hour: number, minute: string) {
        const appointments = this.getAppointmentsForTimeSlot(hour, minute);
        const dialogRef = this.dialog.open(ChoicePopupComponent, {
            data: {
                text: [`Approve or Reject appointment for ${hour}:${minute}?`],
                continueBtnText: 'Approve',
                breakBtnText: 'Reject',
                continueBtnColor: 'primary',
                breakBtnColor: 'warn',
                breakBtnFocus: 'break',
            },
            autoFocus: false,
        });
        dialogRef.closed.subscribe((result) => {
            if (result === 'true') {
                this.client
                    .considerAppointment(appointments[0].id, 'Approved')
                    .subscribe({
                        next: () => {
                            this.showSnackBar(
                                'Appointment approved successfully',
                                'success'
                            );
                        },
                        error: (error) => {
                            if (
                                JSON.parse(error) &&
                                JSON.parse(error)?.errors?.length
                            ) {
                                this.errorMessage =
                                    JSON.parse(error)?.errors[0]?.ErrorMessage;
                                this.showSnackBar(this.errorMessage, 'error');
                            }
                        },
                    });
            } else if (result === 'false') {
                this.client
                    .considerAppointment(appointments[0].id, 'Rejected')
                    .subscribe({
                        next: () => {
                            this.showSnackBar(
                                'Appointment rejected successfully',
                                'success'
                            );
                        },
                        error: (error) => {
                            if (
                                JSON.parse(error) &&
                                JSON.parse(error)?.errors?.length
                            ) {
                                this.errorMessage =
                                    JSON.parse(error)?.errors[0]?.ErrorMessage;
                                this.showSnackBar(this.errorMessage, 'error');
                            }
                        },
                    });
            }
        });
    }

    showSnackBar(message: string, messageType: 'success' | 'error') {
        let panelClass = ['custom-snackbar'];
        if (messageType === 'success') {
            panelClass.push('success-snackbar');
        } else if (messageType === 'error') {
            panelClass.push('error-snackbar');
        }

        this.snackBar.open(message, 'Close', {
            duration: 3000,
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
            panelClass: panelClass,
        });
    }
}
