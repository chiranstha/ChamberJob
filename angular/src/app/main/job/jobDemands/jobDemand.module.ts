import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { JobDemandRoutingModule } from './jobDemand-routing.module';
import { JobDemandsComponent } from './jobDemands.component';
import { CreateOrEditJobDemandModalComponent } from './create-or-edit-jobDemand-modal.component';
import { ViewJobDemandModalComponent } from './view-jobDemand-modal.component';

@NgModule({
    declarations: [JobDemandsComponent, CreateOrEditJobDemandModalComponent, ViewJobDemandModalComponent],
    imports: [AppSharedModule, JobDemandRoutingModule, AdminSharedModule],
})
export class JobDemandModule {}
