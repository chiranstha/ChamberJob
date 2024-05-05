import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { EmploymentRoutingModule } from './employment-routing.module';
import { EmploymentsComponent } from './employments.component';
import { CreateOrEditEmploymentModalComponent } from './create-or-edit-employment-modal.component';
import { ViewEmploymentModalComponent } from './view-employment-modal.component';

@NgModule({
    declarations: [EmploymentsComponent, CreateOrEditEmploymentModalComponent, ViewEmploymentModalComponent],
    imports: [AppSharedModule, EmploymentRoutingModule, AdminSharedModule],
})
export class EmploymentModule {}
