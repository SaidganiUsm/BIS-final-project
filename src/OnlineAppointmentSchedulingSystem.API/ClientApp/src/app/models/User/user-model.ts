import { CategoryDto } from '../DTOs/CategoryDto-model';

export interface UpdateUserProfileModel {
    firstName: string | null;
    lastName: string | null;
    phoneNumber: string | null;
    aboutMe: string | null;
}

export interface UserProfileModel {
    id: number;
    firstName: string | null;
    lastName: string | null;
    phoneNumber: string | null;
    aboutMe: string | null;
    expirience: number | null;
    category: CategoryDto | null;
}
