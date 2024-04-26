import { CategoryDto } from './CategoryDto-model';

export interface UserDto {
    id: number;
    firstName: string | null;
    lastName: string | null;
    aboutMe: string | null;
    expirience: number | null;
    category: CategoryDto | null;
}
