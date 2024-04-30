import { Dialog } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { AppointmentDto } from 'src/app/models/Appointments/appointment-model';
import { ChoicePopupComponent } from 'src/app/ui-elements/choice-popup/choice-popup.component';
import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { Client } from 'src/app/web-api-client';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
    appointments!: AppointmentDto[];
    errorMessage!: string;

    constructor(
        private client: Client,
        private dialog: Dialog,
        private snackBar: MatSnackBar
    ) {}

    ngOnInit() {
        this.loadAppointments();
    }

    cancelClickEvent(id: number) {
        const dialogRef = this.dialog.open(ChoicePopupComponent, {
            data: {
                text: [`Are you sure to cancel an appointment?`],
                continueBtnText: 'Yes',
                breakBtnText: 'No',
                continueBtnColor: 'primary',
                breakBtnColor: 'warn',
                breakBtnFocus: 'break',
            },
            autoFocus: false,
        });
        dialogRef.closed.subscribe((result) => {
            if (result === 'true') {
                this.handleCanceling(id);
            }
        });
    }

    handleCanceling(id: number) {
        this.client.cancelAppointment(id).subscribe({
            next: () => {
                this.showSnackBar(
                    'Appointment cancelled successfully',
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

    loadAppointments() {
        this.client.getAllMyAppointments().subscribe({
            next: (appointment) => {
                this.appointments = appointment;
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
