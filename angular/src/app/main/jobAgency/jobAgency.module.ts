import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { JobdemandComponent } from './jobdemand/jobdemand.component';
import { JobAgencyRoutingModule } from './jobAgency-routing.module';
import { CompanyComponent } from './company/company.component';
import { EmployeeComponent } from './employee/employee.component';
import { EmploymentsComponent } from './employments/employments.component';
import { CreateOrEditJobDemandModalComponent } from './jobdemand/create-or-edit-jobDemand-modal.component';
import { NepaliDatepickerModule } from '@app/shared/common/npx-np-datepicker/np-datepicker.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { EditorModule } from 'primeng/editor';

@NgModule({
    declarations: [JobdemandComponent,CompanyComponent,EmployeeComponent, EmploymentsComponent,CreateOrEditJobDemandModalComponent,],
    imports: [AppSharedModule, JobAgencyRoutingModule,AdminSharedModule, FormsModule, ReactiveFormsModule,
        NepaliDatepickerModule, NgSelectModule, EditorModule ],
   schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class JobAgencyModule {}
