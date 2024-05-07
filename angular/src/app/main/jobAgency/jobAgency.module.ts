import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { JobdemandComponent } from './jobdemand/jobdemand.component';
import { JobAgencyRoutingModule } from './jobAgency-routing.module';
import { CompanyComponent } from './company/company.component';
import { EmployeeComponent } from './employee/employee.component';
import { EmploymentsComponent } from './employments/employments.component';

@NgModule({
    declarations: [JobdemandComponent,CompanyComponent,EmployeeComponent,EmploymentsComponent],
    imports: [AppSharedModule, JobAgencyRoutingModule, AdminSharedModule],
})
export class JobAgencyModule {}
