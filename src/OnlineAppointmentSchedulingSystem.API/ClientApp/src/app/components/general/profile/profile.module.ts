import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UpdateUserProfileComponent } from './update-user-profile/update-user-profile.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { DialogModule } from '@angular/cdk/dialog';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { UiElementsModule } from 'src/app/ui-elements/ui-elements.module';
import { PublicUserProfileComponent } from './public-user-profile/public-user-profile.component';

@NgModule({
    declarations: [
        ChangePasswordComponent,
        UserProfileComponent,
        UpdateUserProfileComponent,
        PublicUserProfileComponent,
    ],
    imports: [
        CommonModule,
        MatButtonModule,
        MatIconModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule,
        ProfileRoutingModule,
        MatProgressSpinnerModule,
        DialogModule,
        UiElementsModule,
    ],
})
export class ProfileModule {}
