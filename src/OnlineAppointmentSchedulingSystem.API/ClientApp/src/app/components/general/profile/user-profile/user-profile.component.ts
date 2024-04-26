import { Dialog } from '@angular/cdk/dialog';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/app/api-authorization/authorize.service';
import { Client } from 'src/app/web-api-client';
import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { UserProfileModel } from 'src/app/models/User/user-model';

@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.scss'],
})
export class UserProfileComponent implements OnInit {
    userProfileData: UserProfileModel | null = null;

    constructor(
        private authorizeService: AuthorizeService,
        private client: Client,
        public dialog: Dialog,
        private router: Router
    ) {}

    isUserDoctor(): boolean {
        return this.authorizeService.isUserDoctor();
    }

    ngOnInit(): void {
        this.fetchUserProfileData();
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

    openDialog(text: string[], error: boolean) {
        const dialogRef = this.dialog.open<string>(DialogPopupComponent, {
            data: {
                text,
                isError: error,
            },
        });

        dialogRef.closed.subscribe((res) => {});
    }
}
