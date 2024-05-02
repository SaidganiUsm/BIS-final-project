import { CategoryDto } from '../DTOs/CategoryDto-model';

export interface DoctorModel {
    id: number;
    firstName: string;
    lastName: string;
    category: CategoryDto;
}
