import { Dialog } from '@angular/cdk/dialog';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/app/api-authorization/authorize.service';
import { UserDto } from 'src/app/models/DTOs/UserDto-model';
import {
    UpdateUserProfileModel,
    UserProfileModel,
} from 'src/app/models/User/user-model';
import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { Client } from 'src/app/web-api-client';

export interface ProfileFormModel {
    firstName: FormControl<string | null>;
    lastName: FormControl<string | null>;
    phoneNumber: FormControl<string | null>;
    aboutMe: FormControl<string | null>;
}

@Component({
    selector: 'app-update-user-profile',
    templateUrl: './update-user-profile.component.html',
    styleUrls: ['./update-user-profile.component.scss'],
})
export class UpdateUserProfileComponent {
    userProfileData: UserDto | null = null;
    isLoading = false;

    constructor(
        private authorizeService: AuthorizeService,
        private client: Client,
        private router: Router,
        private dialog: Dialog
    ) {}

    ngOnInit(): void {
        this.fetchUserProfileData();
    }

    profileForm = new FormGroup<ProfileFormModel>({
        firstName: new FormControl<string>('', [
            Validators.required,
            Validators.pattern(/^[a-zA-Z]*$/),
            Validators.maxLength(30),
            this.noSpaceValidator.bind(this),
        ]),
        lastName: new FormControl<string>('', [
            Validators.required,
            Validators.pattern(/^[a-zA-Z]*$/),
            Validators.maxLength(30),
            this.noSpaceValidator.bind(this),
        ]),
        aboutMe: new FormControl<string>('', [
            Validators.required,
            Validators.minLength(30),
        ]),
        phoneNumber: new FormControl<string>('', [
            Validators.required,
            Validators.pattern(/^\+\d{1,15}$/),
        ]),
    });

    noSpaceValidator(control: FormControl): { [key: string]: any } | null {
        if (this.hasSpace(control.value)) {
            return { noSpace: true };
        }
        return null;
    }

    hasSpace(value: string | null): boolean {
        return value !== null && /\s/.test(value);
    }

    openDialog(text: string[], error: boolean) {
        const dialogRef = this.dialog.open<string>(DialogPopupComponent, {
            data: {
                text,
                isError: error,
            },
        });

        dialogRef.closed.subscribe(() => {});
    }

    private fetchUserProfileData() {
        this.client.getUserData().subscribe({
            next: (data: UserProfileModel) => {
                this.userProfileData = data;
                this.setFormControlData(data);
            },
            error: (error) => {
                this.openDialog(
                    error.errors || [
                        'Something went wrong, please try again later',
                    ],
                    true
                );
            },
        });
    }

    setFormControlData(userProfileData: UserProfileModel | null = null) {
        if (userProfileData) {
            this.userProfileData = userProfileData;
            this.profileForm.setValue({
                firstName: userProfileData.firstName || '',
                lastName: userProfileData.lastName || '',
                aboutMe: userProfileData.aboutMe || '',
                phoneNumber: userProfileData.phoneNumber || '',
            });
        }
    }

    OnSubmit() {
        this.isLoading = true;

        if (this.profileForm.invalid) {
            this.isLoading = false;
            return;
        }

        const formValue = this.profileForm.value;

        const updateProfileModel: UpdateUserProfileModel = {
            firstName: formValue.firstName || null,
            lastName: formValue.lastName || null,
            phoneNumber: formValue.phoneNumber || null,
            aboutMe: formValue.aboutMe || null,
        };

        this.client.updateProfile(updateProfileModel).subscribe({
            next: (result) => {
                if (result) {
                    this.router.navigate(['/profile']);
                }
            },
            complete: () => {
                this.isLoading = false;
            },
        });
    }

    isUserPatient(): boolean {
        return this.authorizeService.isUserPatient();
    }
}
