import { Component, OnInit, computed, effect } from '@angular/core';
import { AuthorizeService } from './api-authorization/authorize.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent {
    isUserDoctor: boolean = false;
    isUserPatient: boolean = false;

    private isDoctorEffect = effect(() => {
        this.isUserDoctor = this.authService.isUserDoctor();
    });

    private isPatientEffect = effect(() => {
        this.isUserPatient = this.authService.isUserPatient();
    });

    constructor(private authService: AuthorizeService) {}

    title = 'Auctionify';
}
