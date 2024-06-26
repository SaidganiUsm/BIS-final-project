import { TestBed } from '@angular/core/testing';

import { AuthorizeInterceptor } from './authorize.interceptor';

describe('AuthorizeInterceptor', () => {
    beforeEach(() =>
        TestBed.configureTestingModule({
            providers: [AuthorizeInterceptor],
        })
    );

    it('should be created', () => {
        const interceptor: AuthorizeInterceptor =
            TestBed.inject(AuthorizeInterceptor);
        expect(interceptor).toBeTruthy();
    });
});
