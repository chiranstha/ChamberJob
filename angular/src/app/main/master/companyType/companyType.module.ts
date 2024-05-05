import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { CompanyTypeRoutingModule } from './companyType-routing.module';
import { CompanyTypeComponent } from './companyType.component';
import { CreateOrEditCompanyTypeModalComponent } from './create-or-edit-companyType-modal.component';
import { ViewCompanyTypeModalComponent } from './view-companyType-modal.component';

@NgModule({
    declarations: [CompanyTypeComponent, CreateOrEditCompanyTypeModalComponent, ViewCompanyTypeModalComponent],
    imports: [AppSharedModule, CompanyTypeRoutingModule, AdminSharedModule],
})
export class CompanyTypeModule {}
