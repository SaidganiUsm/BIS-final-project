import { Dialog } from '@angular/cdk/dialog';
import { Component, OnInit, effect } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthorizeService } from 'src/app/api-authorization/authorize.service';
import { GetUserById, UserProfileModel } from 'src/app/models/User/user-model';
import { DialogPopupComponent } from 'src/app/ui-elements/dialog-popup/dialog-popup.component';
import { Client } from 'src/app/web-api-client';

@Component({
    selector: 'app-public-user-profile',
    templateUrl: './public-user-profile.component.html',
    styleUrls: ['./public-user-profile.component.scss'],
})
export class PublicUserProfileComponent implements OnInit {
    userProfileData: UserProfileModel | null = null;
    currentUserId: number = 0;

    constructor(
        private authorizeService: AuthorizeService,
        private client: Client,
        public dialog: Dialog,
        private activeRoute: ActivatedRoute,
        private router: Router
    ) {
        effect(() => {
            this.currentUserId = this.authorizeService.getUserId()!;
        });
    }

    isUserDoctor(): boolean {
        return this.authorizeService.isUserDoctor();
    }

    ngOnInit(): void {
        this.fetchUserProfileData();
    }

    private fetchUserProfileData() {
        const userId = this.activeRoute.snapshot.params['id'];

        if (this.currentUserId == userId) {
            this.router.navigate(['profile']);
        }

        this.client.getUserById(userId).subscribe({
            next: (data: GetUserById) => {
                this.userProfileData = data;
            },
            error: (error) =>
                this.openDialog(
                    error.errors! || [
                        'Something went wrong, please try again later',
                    ],
                    true
                ),
        });
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
