import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthorizeService } from 'src/app/api-authorization/authorize.service';

export const isStaffGuard: CanActivateFn = (route, state) => {
    const authService: AuthorizeService = inject(AuthorizeService);
    const router = inject(Router);

    if (authService.isUserStaff()) return true;
    return router.parseUrl('home');
};
