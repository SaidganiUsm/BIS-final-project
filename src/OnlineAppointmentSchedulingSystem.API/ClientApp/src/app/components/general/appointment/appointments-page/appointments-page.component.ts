import { Component, Inject, LOCALE_ID, OnInit, effect } from '@angular/core';
import {
    AppointmentDto,
    AppointmentDtoForDoctor,
} from 'src/app/models/Appointments/appointment-model';
import { Client } from 'src/app/web-api-client';

import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { Dialog } from '@angular/cdk/dialog';
import { AuthorizeService } from 'src/app/api-authorization/authorize.service';
import { ChoicePopupComponent } from 'src/app/ui-elements/choice-popup/choice-popup.component';
import { formatDate } from '@angular/common';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
    selector: 'app-appointments-page',
    templateUrl: './appointments-page.component.html',
    styleUrls: ['./appointments-page.component.scss'],
})
export class AppointmentsPageComponent implements OnInit {
    userAPpointmentsForDoctor: AppointmentDtoForDoctor[] = [];
    date = new Date();

    isUserDoctor: boolean = false;
    isUserPatient: boolean = false;

    errorMessage!: string;

    constructor(
        private client: Client,
        public dialog: Dialog,
        private authService: AuthorizeService,
        @Inject(LOCALE_ID) public locale: string,
        private snackBar: MatSnackBar
    ) {
        this.date = new Date(
            Date.UTC(
                new Date().getFullYear(),
                new Date().getMonth(),
                new Date().getDate(),
                new Date().getHours(),
                new Date().getMinutes(),
                new Date().getSeconds(),
                new Date().getMilliseconds()
            )
        );
    }

    ngOnInit() {
        this.fetchUserAppointments();
    }

    clickEventOnApproveAll() {
        const dialogRef = this.dialog.open(ChoicePopupComponent, {
            data: {
                text: [
                    `Are you sure to create an appointment for ${this.formatDate(
                        this.date
                    )}?`,
                ],
                continueBtnText: 'Approve',
                breakBtnText: 'Cancel',
                continueBtnColor: 'primary',
                breakBtnColor: 'warn',
                breakBtnFocus: 'break',
            },
            autoFocus: false,
        });
        dialogRef.closed.subscribe((result) => {
            if (result === 'true') {
                this.handleApproveAll(this.date);
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

    handleApproveAll(date: Date) {
        this.client.considerAllAppointments(date).subscribe({
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

    formatDate(date: Date | null): string {
        return date ? formatDate(date, 'dd LLLL, HH:mm (z)', this.locale) : '';
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

    fetchUserAppointments() {
        this.client.getDoctorAppointmentsToConsider(this.date).subscribe({
            next: (appointments) => {
                this.userAPpointmentsForDoctor = appointments;
            },
            error: (error) => {
                this.openDialog(
                    error.errors || [
                        'Something went wrong, please try again later',
                    ],
                    true
                );
            },
        });
    }

    getAppointmentsForTimeSlot(
        hour: number,
        minute: string
    ): AppointmentDtoForDoctor[] {
        return this.userAPpointmentsForDoctor.filter((appointment) => {
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
                    return 'blue';
                case 'PendingApproval':
                    return 'grey';
                case 'Rejected':
                case 'Cancelled':
                    return 'red';
                default:
                    return 'black';
            }
        } else {
            return 'white';
        }
    }

    changeDate(days: number) {
        const newDate = new Date(this.date);
        newDate.setDate(newDate.getDate() + days);
        this.date = newDate;
        this.fetchUserAppointments();
    }

    private IsUserDoctor = effect(() => {
        this.isUserDoctor = this.authService.isUserDoctor();
    });

    private IsUserPatient = effect(() => {
        this.isUserPatient = this.authService.isUserPatient();
    });
}
