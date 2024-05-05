import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { RegisterRoutingModule } from './register-routing.module';
import { AccountSharedModule } from '@account/shared/account-shared.module';
import { RegisterComponent } from './register.component';
import { PasswordModule } from 'primeng/password';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    imports: [AppSharedModule, AccountSharedModule, FormsModule,ReactiveFormsModule, RegisterRoutingModule, PasswordModule],
    declarations: [RegisterComponent],
})
export class RegisterModule {}
