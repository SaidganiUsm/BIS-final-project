import { Dialog } from '@angular/cdk/dialog';
import { Component, OnInit } from '@angular/core';
import {
    FormGroup,
    FormControl,
    Validators,
    ValidatorFn,
    AbstractControl,
    ValidationErrors,
} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/app/api-authorization/authorize.service';
import { UserDto } from 'src/app/models/DTOs/UserDto-model';
import { UserProfileModel } from 'src/app/models/User/user-model';
import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { Client, ChangePasswordResponse } from 'src/app/web-api-client';

export interface ChangeUserPasswordFormModel {
    oldPassword: FormControl<string | null>;
    newPassword: FormControl<string | null>;
    confirmNewPassword: FormControl<string | null>;
}

@Component({
    selector: 'app-change-password',
    templateUrl: './change-password.component.html',
    styleUrls: ['./change-password.component.scss'],
})
export class ChangePasswordComponent implements OnInit {
    userProfileData: UserDto | null = null;
    isLoading = false;
    passwordHidden: boolean = true;

    constructor(
        private authorizeService: AuthorizeService,
        private client: Client,
        private router: Router,
        public dialog: Dialog
    ) {}

    ngOnInit(): void {
        this.fetchUserProfileData();
    }

    changePasswordForm = new FormGroup<ChangeUserPasswordFormModel>(
        {
            oldPassword: new FormControl<string>('', [
                Validators.required,
                Validators.minLength(8),
            ]),
            newPassword: new FormControl<string>('', [
                Validators.required,
                Validators.minLength(8),
                this.passwordValidator(),
            ]),
            confirmNewPassword: new FormControl<string>('', [
                Validators.required,
                Validators.minLength(8),
                this.passwordValidator(),
            ]),
        },
        {
            validators: this.passwordMatchValidator(),
        }
    );

    togglePasswordVisibility() {
        this.passwordHidden = !this.passwordHidden;
    }

    OnSubmit() {
        this.isLoading = true;

        if (this.changePasswordForm.invalid) {
            this.isLoading = false;
            return;
        }

        this.authorizeService
            .changePassword(
                this.changePasswordForm.controls.oldPassword.value!,
                this.changePasswordForm.controls.newPassword.value!,
                this.changePasswordForm.controls.confirmNewPassword.value!
            )
            .subscribe({
                next: (result) => {
                    this.router.navigate(['/profile']);
                },
                error: (error: ChangePasswordResponse) => {
                    this.openDialog(
                        error.errors! || [
                            'Something went wrong, please try again later',
                        ],
                        true
                    );
                },
            });
    }

    passwordValidator(): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            const password: string = control.value || '';

            const hasNumber = /[0-9]/.test(password);
            const hasUppercase = /[A-Z]/.test(password);
            const hasLowercase = /[a-z]/.test(password);
            const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\/-]/.test(
                password
            );

            const isValid =
                hasNumber &&
                hasUppercase &&
                hasLowercase &&
                hasSpecialChar &&
                password.length >= 8;

            return isValid ? null : { invalidPassword: true };
        };
    }

    passwordMatchValidator(): ValidatorFn {
        return (group: AbstractControl): ValidationErrors | null => {
            const newPassword = group.get('newPassword')?.value;
            const confirmNewPassword = group.get('confirmNewPassword')?.value;

            return newPassword === confirmNewPassword
                ? null
                : { passwordMismatch: true };
        };
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
        });
    }

    private fetchUserProfileData() {
        this.client.getUserData().subscribe(
            (data: UserProfileModel) => {
                this.userProfileData = data;
            },
            (error) => {
                this.openDialog(
                    error.errors! || [
                        'Something went wrong, please try again later',
                    ],
                    true
                );
            }
        );
    }
}
