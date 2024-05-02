import { Component, Injectable, NgZone } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Dialog } from '@angular/cdk/dialog';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';

import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { LoginResponse } from 'src/app/web-api-client';
import { AuthorizeService } from '../authorize.service';

@Injectable({
    providedIn: 'root',
})
@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
    clientId!: string;
    passwordHidden: boolean = true;
    isLoading = false;

    constructor(
        private authService: AuthorizeService,
        public dialog: Dialog,
        private router: Router,
        private _ngZone: NgZone
    ) {}
    loginForm = new FormGroup({
        email: new FormControl<string>('', [
            Validators.required,
            Validators.email,
        ]),
        password: new FormControl('', [Validators.required]),
    });

    togglePasswordVisibility() {
        this.passwordHidden = !this.passwordHidden;
    }

    onSubmit() {
        this.isLoading = true;
        if (this.loginForm.invalid) {
            this.isLoading = false;
            return;
        }

        this.authService
            .login(
                this.loginForm.controls.email.value!,
                this.loginForm.controls.password.value!
            )
            .subscribe({
                next: (result) => {
                    if (this.authService.isUserLoggedIn()) {
                        if (
                            this.authService.isUserDoctor() ||
                            this.authService.isUserPatient() ||
                            this.authService.isUserStaff()
                        ) {
                            this.router.navigate(['/home']);
                            window.location.reload();
                        } else {
                            this.router.navigate(['/home']);
                            window.location.reload();
                        }
                    }
                },
                error: (error: LoginResponse) => {
                    this.openDialog(error.errors!, true);
                },
            });
    }

    openDialog(text: string[], error: boolean) {
        const dialogRef = this.dialog.open<string>(DialogPopupComponent, {
            data: {
                text,
                isError: error,
            },
        });

        dialogRef.closed.subscribe((res) => {
            this.isLoading = false;
            this.loginForm.controls.password.reset();
        });
    }
}
