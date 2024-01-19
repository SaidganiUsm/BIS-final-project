import { mergeMap, catchError } from 'rxjs/operators';
import { Observable, throwError, of } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserRole } from './api-authorization/authorize.service';

export const API_BASE_URL = new InjectionToken('API_BASE_URL');

@Injectable({
    providedIn: 'root',
})
export class Client {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined =
        undefined;

    constructor(
        @Inject(HttpClient) http: HttpClient,
        @Optional() @Inject(API_BASE_URL) baseUrl?: string
    ) {
        this.http = http;
        this.baseUrl = baseUrl !== undefined && baseUrl !== null ? baseUrl : '';
    }

    login(body: LoginViewModel | undefined): Observable<LoginResponse> {
        let url_ = this.baseUrl + '/api/auth/login';

        const content_ = JSON.stringify(body);

        let options_: Object = {
            body: content_,
            observe: 'response',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                Accept: 'text/json',
            }),
        };

        return this.http
            .request('post', url_, options_)
            .pipe(
                mergeMap((response: any): Observable<LoginResponse> => {
                    let data: LoginResponse = {};

                    if (response.body !== null) {
                        data = response.body;
                    }

                    return of(data);
                })
            )
            .pipe(
                catchError((error) => {
                    return throwError(() => error);
                })
            );
    }

    register(
        body: RegisterViewModel | undefined
    ): Observable<RegisterResponse> {
        let url_ = this.baseUrl + '/api/auth/register';

        const content_ = JSON.stringify(body);

        let options_: any = {
            body: content_,
            observe: 'response',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                Accept: 'text/json',
            }),
        };

        return this.http.request('post', url_, options_).pipe(
            mergeMap((response: any): Observable<RegisterResponse> => {
                let data: RegisterResponse = {};

                if (response.body !== null) {
                    data = response.body;
                }

                return of(data);
            })
        );
    }

    resetPassword(
        body: ResetPasswordViewModel | undefined
    ): Observable<ResetPasswordResponse> {
        let url_ = this.baseUrl + '/api/auth/reset-password';

        const content_ = JSON.stringify(body);

        let options_: any = {
            body: content_,
            observe: 'response',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                Accept: 'text/json',
            }),
        };

        return this.http.request('post', url_, options_).pipe(
            mergeMap((response: any): Observable<ResetPasswordResponse> => {
                let data: ResetPasswordResponse = {};

                if (response.body !== null) {
                    data = response.body;
                }

                return of(data);
            })
        );
    }

    forgetPassword(email: string): Observable<ForgetPasswordResponse> {
        let url_ = `${
            this.baseUrl
        }/api/auth/forget-password?email=${encodeURIComponent(email)}`;

        console.log(email);

        let options_: any = {
            observe: 'response',
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                Accept: 'text/json',
            }),
        };

        return this.http.request('post', url_, options_).pipe(
            mergeMap((response: any): Observable<ForgetPasswordResponse> => {
                let data: ForgetPasswordResponse = {};

                if (response.body !== null) {
                    data = response.body;
                }

                return of(data);
            })
        );
    }

    changePassword(body: ChangeUserPasswordModel): Observable<any> {
        let url_ = this.baseUrl + `/api/auth/change-password`;

        const formData = new FormData();

        formData.append('oldPassword', body.oldPassword ?? '');
        formData.append('newPassword', body.newPassword ?? '');
        formData.append('confirmNewPassword', body.confirmNewPassword ?? '');

        let options_: any = {
            body: formData,
        };

        return this.http.request('put', url_, options_).pipe(
            catchError((error) => {
                return throwError(() => error.error);
            })
        );
    }
}

export interface TokenModel {
    accessToken: string;
    expireDate: string;
    role: UserRole;
    userId: number;
}

export interface RegisterResponse {
    message?: string | undefined;
    isSuccess?: boolean;
    errors?: string[] | undefined;
}

export interface ChangePasswordResponse {
    message?: string | undefined;
    isSuccess?: boolean;
    errors?: string[] | undefined;
}

export interface ForgetPasswordResponse {
    message?: string | undefined;
    isSuccess?: boolean;
    errors?: string[] | undefined;
}

export interface ResetPasswordResponse {
    message?: string | undefined;
    isSuccess?: boolean;
    errors?: string[] | undefined;
}

export interface LoginViewModel {
    email: string;
    password: string;
}

export interface RegisterViewModel {
    email: string;
    password: string;
    confirmPassword: string;
}

export interface ForgetPasswordViewModel {
    email: string;
}

export interface ResetPasswordViewModel {
    token: string;
    email: string;
    newPassword: string;
    confirmPassword: string;
}

export interface ChangeUserPasswordModel {
    oldPassword: string | null;
    newPassword: string | null;
    confirmNewPassword: string | null;
}

export interface LoginResponse {
    message?: string | undefined;
    isSuccess?: boolean;
    errors?: string[] | undefined;
    result?: TokenModel;
}

export class ApiException extends Error {
    override message: string;
    status: number;
    response: string;
    headers: { [key: string]: any };
    result: any;

    constructor(
        message: string,
        status: number,
        response: string,
        headers: { [key: string]: any },
        result: any
    ) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(
    message: string,
    status: number,
    response: string,
    headers: { [key: string]: any },
    result?: any
): Observable<any> {
    if (result !== null && result !== undefined)
        return throwError(() => result);
    else
        return throwError(
            () => new ApiException(message, status, response, headers, null)
        );
}