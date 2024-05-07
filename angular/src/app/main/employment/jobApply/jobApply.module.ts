import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { JobApplyRoutingModule } from './jobApply-routing.module';
import { JobApplyComponent } from './jobApply.component';
import { CreateOrEditJobApplyModalComponent } from './create-or-edit-jobApply-modal.component';
import { ViewJobApplyModalComponent } from './view-jobApply-modal.component';

@NgModule({
    declarations: [JobApplyComponent, CreateOrEditJobApplyModalComponent, ViewJobApplyModalComponent],
    imports: [AppSharedModule, JobApplyRoutingModule, AdminSharedModule],
})
export class JobApplyModule {}
