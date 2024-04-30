import { Dialog } from '@angular/cdk/dialog';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorModel } from 'src/app/models/Doctor/Doctor';
import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { Client } from 'src/app/web-api-client';

@Component({
    selector: 'app-doctors-list-page',
    templateUrl: './doctors-list-page.component.html',
    styleUrls: ['./doctors-list-page.component.scss'],
})
export class DoctorsListPageComponent implements OnInit {
    doctors: DoctorModel[] = [];

    constructor(
        private client: Client,
        public dialog: Dialog,
        public router: Router
    ) {}

    ngOnInit(): void {
        this.fetchDoctors();
    }

    fetchDoctors() {
        this.client.getDoctors().subscribe({
            next: (doctors) => {
                this.doctors = doctors;
                console.log(doctors);
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

    openDialog(text: string[], error: boolean) {
        const dialogRef = this.dialog.open<string>(DialogPopupComponent, {
            data: {
                text,
                isError: error,
            },
        });

        dialogRef.closed.subscribe(() => {});
    }

    showProfile(doctorId: number) {
        this.router.navigate(['/user', doctorId]);
    }

    showDoctorCalendar(doctorId: number) {
        this.router.navigate(['/appointment/doctor', doctorId]);
    }
}
