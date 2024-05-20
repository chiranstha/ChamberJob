import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { JobDemandRoutingModule } from './jobDemand-routing.module';
import { JobDemandsComponent } from './jobDemands.component';
import { CreateOrEditJobDemandModalComponent } from './create-or-edit-jobDemand-modal.component';
import { ViewJobDemandModalComponent } from './view-jobDemand-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NepaliDatepickerModule } from '@app/shared/common/npx-np-datepicker/np-datepicker.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { EditorModule } from 'primeng/editor';

@NgModule({
    declarations: [JobDemandsComponent, CreateOrEditJobDemandModalComponent, ViewJobDemandModalComponent],
    imports: [AppSharedModule, JobDemandRoutingModule, AdminSharedModule, FormsModule, ReactiveFormsModule,
         NepaliDatepickerModule, NgSelectModule, EditorModule ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class JobDemandModule {}
