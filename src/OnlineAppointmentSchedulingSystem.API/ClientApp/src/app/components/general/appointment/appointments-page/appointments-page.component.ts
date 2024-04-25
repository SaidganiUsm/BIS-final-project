import { Component, OnInit } from '@angular/core';
import { AppointmentDto } from 'src/app/models/Appointments/appointment-model';
import { Client } from 'src/app/web-api-client';

import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { Dialog } from '@angular/cdk/dialog';

@Component({
    selector: 'app-appointments-page',
    templateUrl: './appointments-page.component.html',
    styleUrls: ['./appointments-page.component.scss'],
})
export class AppointmentsPageComponent implements OnInit {
    userAppointments: AppointmentDto[] = [];

    date = new Date();

    constructor(private client: Client, public dialog: Dialog) {
        this.date = new Date();
    }

    ngOnInit() {
        this.fetchUserAppointments();
    }

    hours: number[] = Array.from({ length: 13 }, (_, i) => i + 8);
    minutes: string[] = Array.from({ length: 6 }, (_, i) =>
        (i * 10).toString()
    );

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
        this.client.getUserAppointments(this.date).subscribe({
            next: (appointments) => {
                this.userAppointments = appointments;
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

    getAppointmentsForTimeSlot(hour: number, minute: string): AppointmentDto[] {
        return this.userAppointments.filter((appointment) => {
            // Extract hour and minute from appointment time
            const appointmentTime = new Date(appointment.date);
            const appointmentHour = appointmentTime.getHours();
            const appointmentMinute = appointmentTime.getMinutes();

            // Check if appointment hour and minute match the provided hour and minute
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
}
