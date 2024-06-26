import { Injectable, WritableSignal, computed, signal } from '@angular/core';
import { Observable, catchError, map, of, throwError } from 'rxjs';
import {
    ChangePasswordResponse,
    ChangeUserPasswordModel,
    Client,
    ForgetPasswordResponse,
    ForgetPasswordViewModel,
    LoginResponse,
    LoginViewModel,
    RegisterResponse,
    RegisterViewModel,
    ResetPasswordViewModel,
    ResetPasswordResponse,
} from '../web-api-client';
import {
    HttpClient,
    HttpErrorResponse,
    HttpHeaders,
} from '@angular/common/http';

export enum UserRole {
    Administrator = 'Administrator',
    Doctor = 'Doctor',
    Patient = 'Patient',
    User = 'User',
    Staff = 'Staff',
}

export interface IUser {
    userToken: string | null;
    role: WritableSignal<UserRole | null>;
    expireDate: string | null;
    userId: WritableSignal<number | null>;
}

@Injectable({
    providedIn: 'root',
})
export class AuthorizeService {
    private tokenString: string = 'token';
    private expireString: string = 'expires_at';
    private roleString: string = 'role';
    private userIdString: string = 'userId';
    private user: IUser | null = null;

    constructor(private client: Client, private httpClient: HttpClient) {
        this.initializeAuthorizeService();
    }

    private initializeAuthorizeService() {
        const token = localStorage.getItem(this.tokenString);
        const tokenExpireDate = localStorage.getItem(this.expireString);
        const role = localStorage.getItem(this.roleString);
        const userId = Number(localStorage.getItem(this.userIdString));
        if (token === null && tokenExpireDate === null && role === null) {
            this.user = {
                userToken: null,
                expireDate: null,
                role: signal(null),
                userId: signal(null),
            };
        } else {
            const roleEnum: UserRole = role as UserRole;

            const user: IUser = {
                userToken: token!,
                expireDate: tokenExpireDate!,
                role: signal(roleEnum),
                userId: signal(userId),
            };

            this.user = user;
        }
    }

    login(
        email: string,
        password: string
    ): Observable<LoginResponse | boolean> {
        const loginData: LoginViewModel = {
            email,
            password,
        };

        return this.client
            .login(loginData)
            .pipe(
                map((response): boolean => {
                    this.processLoginResponse(response);
                    return true;
                })
            )
            .pipe(
                catchError(
                    (err: HttpErrorResponse): Observable<LoginResponse> => {
                        return throwError(() => err.error);
                    }
                )
            );
    }

    processLoginResponse(response: LoginResponse) {
        if (response.result === undefined) throw new Error('user not found');
        console.log(response);
        localStorage.setItem(this.tokenString, response.result.accessToken);
        localStorage.setItem(this.expireString, response.result.expireDate);
        localStorage.setItem(this.roleString, response.result.role);
        localStorage.setItem(
            this.userIdString,
            response.result.userId.toString()
        );

        if (this.user == null) return false;

        this.user.userToken = response.result.accessToken;
        this.user.expireDate = response.result.expireDate;

        this.user.role.set(response.result?.role as UserRole);
        this.user.userId.set(response.result.userId);

        return true;
    }

    register(
        email: string,
        firstName: string,
        lastName: string,
        password: string,
        confirmPassword: string
    ): Observable<RegisterResponse | undefined> {
        const registerData: RegisterViewModel = {
            email,
            firstName,
            lastName,
            password,
            confirmPassword,
        };

        return this.client.register(registerData).pipe(
            map((result) => {
                return result;
            })
        );
    }

    forgetPassword(
        email: string
    ): Observable<ForgetPasswordResponse | undefined> {
        return this.client.forgetPassword(email).pipe(
            map((result) => {
                return result;
            })
        );
    }

    resetPassword(
        token: string,
        email: string,
        newPassword: string,
        confirmPassword: string
    ): Observable<ResetPasswordResponse | undefined> {
        const resetPasswordData: ResetPasswordViewModel = {
            token,
            email,
            newPassword,
            confirmPassword,
        };

        return this.client.resetPassword(resetPasswordData).pipe(
            map((result) => {
                return result;
            })
        );
    }

    changePassword(
        oldPassword: string,
        newPassword: string,
        confirmNewPassword: string
    ): Observable<ChangePasswordResponse | undefined> {
        const changePasswordData: ChangeUserPasswordModel = {
            oldPassword,
            newPassword,
            confirmNewPassword,
        };

        return this.client.changePassword(changePasswordData).pipe(
            map((result) => {
                return result;
            })
        );
    }

    logout(): boolean {
        localStorage.removeItem(this.tokenString);
        localStorage.removeItem(this.expireString);
        localStorage.removeItem(this.roleString);
        localStorage.removeItem(this.userIdString);
        this.user?.role.set(null);
        this.user?.userId.set(null);
        this.user!.expireDate = null;
        this.user!.userToken = null;

        return true;
    }

    getAccessToken(): Observable<string | null> {
        const localToken = localStorage.getItem(this.tokenString);

        if (localToken === null && localToken !== this.user?.userToken)
            return of(null);
        return of(localToken);
    }

    isUserDoctor = computed(() => {
        return this.user?.role() == UserRole.Doctor;
    });

    isUserPatient = computed(() => {
        return this.user?.role() == UserRole.Patient;
    });

    isUserStaff = computed(() => {
        return this.user?.role() == UserRole.Staff;
    });

    isUserAdmin = computed(() => {
        return this.user?.role() == UserRole.Administrator;
    });

    getUserId = computed(() => this.user?.userId());

    isUserLoggedIn(): boolean {
        return this.user?.userToken !== null && this.getAccessToken() !== null;
    }
}
