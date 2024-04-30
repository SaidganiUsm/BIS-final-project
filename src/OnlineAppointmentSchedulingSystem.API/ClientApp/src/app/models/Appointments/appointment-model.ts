import { UserDto } from '../DTOs/UserDto-model';

export interface AppointmentStatusDto {
    id: number;
    name: string;
}

export interface AppointmentDto {
    id: number;
    date: string;
    doctorId: number;
    doctor: UserDto;
    appointmentStatusId: number;
    appointmentStatus: AppointmentStatusDto;
}

export interface CreateAppointmentModel {
    date: Date;
    doctorId: number;
}

export interface AppointmentDtoForDoctor {
    id: number;
    date: string;
    clientId: number;
    client: UserDto;
    doctorId: number;
    appointmentStatusId: number;
    appointmentStatus: AppointmentStatusDto;
}
