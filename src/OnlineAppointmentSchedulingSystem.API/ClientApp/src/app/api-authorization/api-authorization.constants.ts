export const ApplicationName = 'APSUZ';

export const LoginActions = {
    Login: 'login',
    Profile: 'profile',
    Register: 'register',
    LoginFailed: 'login-failed',
    ForgetPassword: 'forget-password',
    ResetPassword: 'reset-password',
    EmailSent: 'email-sent',
};

let applicationPaths: ApplicationPathsType = {
    Login: `auth/${LoginActions.Login}`,
    Register: `auth/${LoginActions.Register}`,
    Profile: `auth/${LoginActions.Profile}`,
    LoginFailed: `auth/${LoginActions.LoginFailed}`,
    ForgetPassword: `auth/${LoginActions.ForgetPassword}`,
    ResetPassword: `auth/${LoginActions.ResetPassword}`,
    EmailSent: `auth/${LoginActions.EmailSent}/:email`,
    LoginPathComponents: [],
    RegisterPathComponents: [],
    ProfilePathComponents: [],
    ForgetPasswordComponents: [],
    ResetPasswordComponents: [],
    EmailSentComponents: [],
};

applicationPaths = {
    ...applicationPaths,
    LoginPathComponents: applicationPaths.Login.split('/'),
    RegisterPathComponents: applicationPaths.Register.split('/'),
    ProfilePathComponents: applicationPaths.Profile.split('/'),
    ForgetPasswordComponents: applicationPaths.ForgetPassword.split('/'),
    ResetPasswordComponents: applicationPaths.ResetPassword.split('/'),
    EmailSentComponents: applicationPaths.Profile.split('/'),
};

interface ApplicationPathsType {
    readonly Login: string;
    readonly LoginFailed: string;
    readonly Register: string;
    readonly Profile: string;
    readonly ForgetPassword: string;
    readonly ResetPassword: string;
    readonly EmailSent: string;
    readonly LoginPathComponents: string[];
    readonly RegisterPathComponents: string[];
    readonly ProfilePathComponents: string[];
    readonly ForgetPasswordComponents: string[];
    readonly ResetPasswordComponents: string[];
    readonly EmailSentComponents: string[];
}

export const ApplicationPaths: ApplicationPathsType = applicationPaths;
