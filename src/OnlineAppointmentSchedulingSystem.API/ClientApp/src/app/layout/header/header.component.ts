import { Dialog } from '@angular/cdk/dialog';
import { Component, effect } from '@angular/core';
import { Router } from '@angular/router';
import { AuthorizeService } from 'src/app/api-authorization/authorize.service';
import { ChoicePopupComponent } from 'src/app/ui-elements/choice-popup/choice-popup.component';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
    isUserLoggedIn: boolean = false;
    isUserDoctor: boolean = false;
    isUserPatient: boolean = false;
    isUserAdmin: boolean = false;

    private IsUserDoctor = effect(() => {
        this.isUserDoctor = this.authService.isUserDoctor();
    });

    private IsUserPatient = effect(() => {
        this.isUserPatient = this.authService.isUserPatient();
    });

    private IsUserLoggedIn = effect(() => {
        this.isUserLoggedIn = this.authService.isUserLoggedIn();
    });

    private IsUserAdmin = effect(() => {
        this.isUserAdmin = this.authService.isUserAdmin();
    });

    constructor(
        private authService: AuthorizeService,
        private router: Router,
        private dialog: Dialog
    ) {}

    logout() {
        const dialogRef = this.dialog.open(ChoicePopupComponent, {
            data: {
                text: ['Are you sure to log out?'],
                isError: true,
                continueBtnText: 'Log Out',
                breakBtnText: 'Cancel',
                additionalText: 'Just double-check before you go',
                continueBtnColor: 'warn',
                breakBtnColor: 'warn',
                breakBtnFocus: 'break',
            },
            autoFocus: false,
        });
        dialogRef.closed.subscribe((result) => {
            if (result === 'true') {
                this.authService.logout();
                this.router.navigate(['/home']).then(() => {
                    window.location.reload();
                });
            }
        });
    }
}
