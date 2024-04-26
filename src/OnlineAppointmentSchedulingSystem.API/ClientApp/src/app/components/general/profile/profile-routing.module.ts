import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { UpdateUserProfileComponent } from './update-user-profile/update-user-profile.component';
import { UserProfileComponent } from './user-profile/user-profile.component';

const profileRoutes: Routes = [
    {
        path: '',
        component: UserProfileComponent,
        pathMatch: 'full',
    },
    {
        path: 'update-profile',
        component: UpdateUserProfileComponent,
        pathMatch: 'full',
    },
    {
        path: 'change-password',
        component: ChangePasswordComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(profileRoutes)],
    exports: [RouterModule],
})
export class ProfileRoutingModule {}
