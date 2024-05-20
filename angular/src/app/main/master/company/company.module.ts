import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { CompanyRoutingModule } from './company-routing.module';
import { CompanyComponent } from './company.component';
import { CreateOrEditCompanyModalComponent } from './create-or-edit-company-modal.component';
import { ViewCompanyModalComponent } from './view-company-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NepaliDatepickerModule } from '@app/shared/common/npx-np-datepicker/np-datepicker.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { EditorModule } from 'primeng/editor';

@NgModule({
    declarations: [CompanyComponent, CreateOrEditCompanyModalComponent, ViewCompanyModalComponent],
    imports: [AppSharedModule, CompanyRoutingModule, AdminSharedModule, FormsModule, ReactiveFormsModule,
        NepaliDatepickerModule, NgSelectModule, EditorModule],
})
export class CompanyModule {}
