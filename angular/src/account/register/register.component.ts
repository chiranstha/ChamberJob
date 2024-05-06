import { AfterViewInit, Component, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { accountModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import {
    AccountServiceProxy,
    PasswordComplexitySetting,
    ProfileServiceProxy,
    RegisterOutput,
} from '@shared/service-proxies/service-proxies';
import { LoginService } from '../login/login.service';
import { RegisterModel } from './register.model';
import { finalize } from 'rxjs/operators';
import { ReCaptchaV3WrapperService } from '@account/shared/recaptchav3-wrapper.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
    templateUrl: './register.component.html',
    animations: [accountModuleAnimation()],
})
export class RegisterComponent extends AppComponentBase implements OnInit, AfterViewInit {
   
    passwordComplexitySetting: PasswordComplexitySetting = new PasswordComplexitySetting();
    saving = false;

    form: FormGroup;
    constructor(
        injector: Injector,
        private _accountService: AccountServiceProxy,
        private _router: Router,
        private fb: FormBuilder,
        private readonly _loginService: LoginService,
        private _profileService: ProfileServiceProxy,
        private _recaptchaWrapperService: ReCaptchaV3WrapperService
    ) {
        super(injector);
    }

    ngOnInit() {
        //Prevent to register new users in the host context
        if (this.appSession.tenant == null) {
            this._router.navigate(['account/login']);
            return;
        }

        this.createForm();
        this._profileService.getPasswordComplexitySetting().subscribe((result) => {
            this.passwordComplexitySetting = result.setting;
        });
    }


    createForm(item: any = {}) {

        this.form = this.fb.group({
            name: [item.name, Validators.required],
            surname: [item.surname ? item.surname : ""],
            userName: [item.userName, Validators.required],
            emailAddress: [item.emailAddress, Validators.required],

            password: ['', [
                Validators.required,
                Validators.minLength(this.passwordComplexitySetting.requiredLength),
                this.validatePassword.bind(this)
            ]],

            passwordRepeat: [item.passwordRepeat, Validators.required],
            captchaResponse: [item.captchaResponse],
            userType: [2, Validators.required]
        });
    }

    validatePassword(control) {
        const value = control.value;
        const hasDigit = /[0-9]/.test(value);
        const hasLowercase = /[a-z]/.test(value);
        const hasUppercase = /[A-Z]/.test(value);
        const hasNonAlphanumeric = /[^0-9a-zA-Z]/.test(value);

        const valid = this.passwordComplexitySetting.requireDigit ? hasDigit : true &&
            this.passwordComplexitySetting.requireLowercase ? hasLowercase : true &&
                this.passwordComplexitySetting.requireUppercase ? hasUppercase : true &&
                    this.passwordComplexitySetting.requireNonAlphanumeric ? hasNonAlphanumeric : true;

        return valid ? null : { invalidPassword: true };
    }

    ngAfterViewInit(): void {
        this._recaptchaWrapperService.setCaptchaVisibilityOnRegister();
    }

    save(): void {
        if (this.form.invalid) {
            this.message.error(this.l('PleaseCompleteTheForm'));
            console.log(this.form.errors)
            return;
        }
        let recaptchaCallback = (token: string) => {
            this.saving = true;
            this.form.get('captchaResponse').setValue(token);
            this._accountService
                .register(this.form.getRawValue())
                .pipe(
                    finalize(() => {
                        this.saving = false;
                    })
                )
                .subscribe((result: RegisterOutput) => {
                    if (!result.canLogin) {
                        this.notify.success(this.l('SuccessfullyRegistered'));
                        this._router.navigate(['account/login']);
                        return;
                    }

                    //Autheticate
                    this.saving = true;
                    this._loginService.authenticateModel.userNameOrEmailAddress =  this.form.get('userName').value;
                    this._loginService.authenticateModel.password =  this.form.get('password').value;
                    this._loginService.authenticate(() => {
                        this.saving = false;
                    });
                });
        };

        if (this._recaptchaWrapperService.useCaptchaOnRegister()) {
            this._recaptchaWrapperService.getService().execute('register').subscribe((token) => recaptchaCallback(token));
        } else {
            recaptchaCallback(null);
        }
    }
}
