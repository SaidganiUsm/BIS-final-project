import { Component, Injectable, NgZone } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Dialog } from '@angular/cdk/dialog';
import { Router } from '@angular/router';
import { CredentialResponse, PromptMomentNotification } from 'google-one-tap';

import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { RegisterResponse } from 'src/app/web-api-client';
import { AuthorizeService } from '../authorize.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
})
export class RegisterComponent {
    clientId!: string;
    passwordHidden: boolean = true;
    isLoading = false;

    constructor(
        private authService: AuthorizeService,
        public dialog: Dialog,
        private router: Router,
        private service: AuthorizeService,
        private _ngZone: NgZone
    ) {}

    registerForm = new FormGroup({
        email: new FormControl<string>('', [
            Validators.required,
            Validators.email,
        ]),
        firstName: new FormControl('', [Validators.required]),
        lastName: new FormControl('', [Validators.required]),
        password: new FormControl('', [Validators.required]),
        confirmPassword: new FormControl('', [Validators.required]),
    });

    togglePasswordVisibility() {
        this.passwordHidden = !this.passwordHidden;
    }

    onSubmit() {
        this.isLoading = true;
        if (this.registerForm.invalid) {
            this.isLoading = false;
            return;
        }

        this.authService
            .register(
                this.registerForm.controls.email.value!,
                this.registerForm.controls.firstName.value!,
                this.registerForm.controls.lastName.value!,
                this.registerForm.controls.password.value!,
                this.registerForm.controls.confirmPassword.value!
            )
            .subscribe({
                next: (result) => {
                    this.router.navigate([
                        `auth/email-sent/${this.registerForm.value.email}`,
                    ]);
                },
                error: (response: HttpErrorResponse) => {
                    if (response.error.errors.Password) {
                        this.openDialog([response.error.errors.Password], true);
                    } else if (response.error.errors.ConfirmPassword) {
                        this.openDialog(
                            response.error.errors.ConfirmPassword,
                            true
                        );
                    } else if (response.error.errors.Email) {
                        this.openDialog([response.error.errors.Email], true);
                    } else {
                        this.openDialog(
                            ['Something went wrong please try again later'],
                            true
                        );
                    }
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
            this.registerForm.controls.password.reset();
            this.registerForm.controls.confirmPassword.reset();
        });
    }
}
