import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { EmploymentRoutingModule } from './employment-routing.module';
import { JobdemandComponent } from './jobdemand/jobdemand.component';
import { EmployeeComponent } from './employee/employee.component';
import { CreateOrEditEmployeeModalComponent } from './employee/create-or-edit-employee-modal.component';
import { ViewEmployeeModalComponent } from './employee/view-employee-modal.component';
import { JobApplyComponent } from './jobApply/jobApply.component';
import { CreateOrEditJobApplyModalComponent } from './jobApply/create-or-edit-jobApply-modal.component';
import { ViewJobApplyModalComponent } from './jobApply/view-jobApply-modal.component';

@NgModule({
    declarations: [JobdemandComponent,EmployeeComponent, CreateOrEditEmployeeModalComponent, ViewEmployeeModalComponent,JobApplyComponent, CreateOrEditJobApplyModalComponent, ViewJobApplyModalComponent],
    imports: [AppSharedModule, EmploymentRoutingModule, AdminSharedModule,],
})
export class EmploymentModule {}
