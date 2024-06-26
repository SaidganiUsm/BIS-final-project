import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ReactiveFormsModule } from '@angular/forms';
import { DialogModule } from '@angular/cdk/dialog';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { FormsModule } from '@angular/forms';

import { ApplicationPaths } from '../api-authorization/api-authorization.constants';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { EmailSentComponent } from './email-sent/email-sent.component';
import { ForgetPasswordComponent } from './forget-password/forget-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { isLoggedInGuard } from '../guards/is-logged-in.guard';
import { UiElementsModule } from '../ui-elements/ui-elements.module';

@NgModule({
    declarations: [
        RegisterComponent,
        LoginComponent,
        EmailSentComponent,
        ForgetPasswordComponent,
        ResetPasswordComponent,
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        RouterModule.forChild([
            {
                path: ApplicationPaths.Login,
                component: LoginComponent,
                canActivate: [isLoggedInGuard],
            },
            {
                path: ApplicationPaths.Register,
                component: RegisterComponent,
                canActivate: [isLoggedInGuard],
            },
            {
                path: ApplicationPaths.ForgetPassword,
                component: ForgetPasswordComponent,
            },
            {
                path: ApplicationPaths.ResetPassword,
                component: ResetPasswordComponent,
            },
            {
                path: ApplicationPaths.EmailSent,
                component: EmailSentComponent,
            },
        ]),
        UiElementsModule,
        MatFormFieldModule,
        MatInputModule,
        MatIconModule,
        ReactiveFormsModule,
        DialogModule,
        MatButtonModule,
        MatProgressSpinnerModule,
        MatButtonToggleModule,
        FormsModule,
    ],
    exports: [],
})
export class ApiAuthorizationModule {}
