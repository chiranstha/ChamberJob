import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { EmploymentRoutingModule } from './employment-routing.module';
import { JobdemandComponent } from './jobdemand/jobdemand.component';

@NgModule({
    declarations: [JobdemandComponent],
    imports: [AppSharedModule, EmploymentRoutingModule, AdminSharedModule],
})
export class EmploymentModule {}
