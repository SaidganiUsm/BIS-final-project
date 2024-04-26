import { Component, Input } from '@angular/core';
import { AppointmentDto } from 'src/app/models/Appointments/appointment-model';

@Component({
    selector: 'app-calendar',
    templateUrl: './calendar.component.html',
    styleUrls: ['./calendar.component.scss'],
})
export class CalendarComponent {
    @Input() userAppointments: AppointmentDto[] = [];

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

    hours: number[] = Array.from({ length: 13 }, (_, i) => i + 10);
    minutes: string[] = Array.from({ length: 6 }, (_, i) =>
        (i * 10).toString()
    );
}
