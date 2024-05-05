import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { CompanyRoutingModule } from './company-routing.module';
import { CompanyComponent } from './company.component';
import { CreateOrEditCompanyModalComponent } from './create-or-edit-company-modal.component';
import { ViewCompanyModalComponent } from './view-company-modal.component';

@NgModule({
    declarations: [CompanyComponent, CreateOrEditCompanyModalComponent, ViewCompanyModalComponent],
    imports: [AppSharedModule, CompanyRoutingModule, AdminSharedModule],
})
export class CompanyModule {}
