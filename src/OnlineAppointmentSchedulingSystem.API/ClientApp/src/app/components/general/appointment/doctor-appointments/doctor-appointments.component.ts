import { Dialog } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppointmentDto } from 'src/app/models/Appointments/appointment-model';
import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { Client } from 'src/app/web-api-client';
import { formatDate } from '@angular/common';

@Component({
    selector: 'app-doctor-appointments',
    templateUrl: './doctor-appointments.component.html',
    styleUrls: ['./doctor-appointments.component.scss'],
})
export class DoctorAppointmentsComponent {
    doctorAppointments: AppointmentDto[] = [];
    doctorId!: number;

    date = new Date();

    constructor(
        private client: Client,
        public dialog: Dialog,
        private activeRoute: ActivatedRoute
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
        this.fetchDoctorAppointments();
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

    fetchDoctorAppointments() {
        const id = this.activeRoute.snapshot.params['id'];
        this.doctorId = id;
        this.client.getDoctorAppointments(id, this.date).subscribe({
            next: (appointments) => {
                this.doctorAppointments = appointments;
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

    changeDate(days: number) {
        const newDate = new Date(this.date);
        newDate.setUTCDate(newDate.getUTCDate() + days);
        this.date = newDate;
        this.fetchDoctorAppointments();
    }
}
