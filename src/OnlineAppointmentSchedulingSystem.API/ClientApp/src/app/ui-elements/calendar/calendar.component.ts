import { Dialog } from '@angular/cdk/dialog';
import { Component, Input, effect } from '@angular/core';
import {
    AppointmentDto,
    CreateAppointmentModel,
} from 'src/app/models/Appointments/appointment-model';
import { ChoicePopupComponent } from '../choice-popup/choice-popup.component';
import { Client } from 'src/app/web-api-client';
import { DialogPopupComponent } from '../dialog-popup/dialog-popup.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthorizeService } from 'src/app/api-authorization/authorize.service';

@Component({
    selector: 'app-calendar',
    templateUrl: './calendar.component.html',
    styleUrls: ['./calendar.component.scss'],
})
export class CalendarComponent {
    @Input() userAppointments: AppointmentDto[] = [];
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

    getAppointmentsForTimeSlot(hour: number, minute: string): AppointmentDto[] {
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
        this.handleCreateAppointment(hour, minute);
    }

    handleCreateAppointment(hour: number, minute: string) {
        const appointments = this.getAppointmentsForTimeSlot(hour, minute);
        if (appointments.length === 0) {
            const dialogRef = this.dialog.open(ChoicePopupComponent, {
                data: {
                    text: [
                        `Are you sure to create an appointment for ${hour}:${minute}?`,
                    ],
                    continueBtnText: 'Create',
                    breakBtnText: 'Cancel',
                    continueBtnColor: 'primary',
                    breakBtnColor: 'warn',
                    breakBtnFocus: 'break',
                },
                autoFocus: false,
            });
            dialogRef.closed.subscribe((result) => {
                if (result === 'true') {
                    this.createAppointment(hour, minute);
                }
            });
        }
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

    openDialog(text: string[], error: boolean) {
        const dialogRef = this.dialog.open<string>(DialogPopupComponent, {
            data: {
                text,
                isError: error,
            },
        });

        dialogRef.closed.subscribe(() => {});
    }

    createAppointment(hour: number, minute: string) {
        const appointmentDate = new Date(this.date);
        appointmentDate.setUTCHours(hour);
        appointmentDate.setMinutes(parseInt(minute));
        const day = appointmentDate.getDate();
        const month = appointmentDate.getMonth();

        appointmentDate.setDate(day);
        appointmentDate.setMonth(month);

        const appointmentData: CreateAppointmentModel = {
            date: appointmentDate,
            doctorId: this.doctorId,
        };

        this.client.createAppointment(appointmentData).subscribe({
            next: () => {
                this.showSnackBar(
                    'Appointment submitted successfully',
                    'success'
                );
            },
            error: (error) => {
                if (JSON.parse(error) && JSON.parse(error)?.errors?.length) {
                    this.errorMessage =
                        JSON.parse(error)?.errors[0]?.ErrorMessage;
                    this.showSnackBar(this.errorMessage, 'error');
                }
            },
        });
    }
}
