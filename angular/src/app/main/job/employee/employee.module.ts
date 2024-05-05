import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { EmployeeRoutingModule } from './employee-routing.module';
import { EmployeeComponent } from './employee.component';
import { CreateOrEditEmployeeModalComponent } from './create-or-edit-employee-modal.component';
import { ViewEmployeeModalComponent } from './view-employee-modal.component';

@NgModule({
    declarations: [EmployeeComponent, CreateOrEditEmployeeModalComponent, ViewEmployeeModalComponent],
    imports: [AppSharedModule, EmployeeRoutingModule, AdminSharedModule],
})
export class EmployeeModule {}
