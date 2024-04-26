import { UserDto } from "../DTOs/UserDto-model";

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
