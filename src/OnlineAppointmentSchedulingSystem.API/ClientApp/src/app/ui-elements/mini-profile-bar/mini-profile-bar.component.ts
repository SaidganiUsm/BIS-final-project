import { Component, Input } from '@angular/core';
import { UserDto } from 'src/app/models/DTOs/UserDto-model';

@Component({
    selector: 'app-mini-profile-bar',
    templateUrl: './mini-profile-bar.component.html',
    styleUrls: ['./mini-profile-bar.component.scss'],
})
export class MiniProfileBarComponent {
    @Input() userModel: UserDto | null = null;
}
