export interface UserDto {
    id: number;
    firstName: string | null;
    lastName: string | null;
    aboutMe: string | null;
    expirience: number | null;
}

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
