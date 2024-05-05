import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { QualificationRoutingModule } from './qualification-routing.module';
import { QualificationComponent } from './qualification.component';
import { CreateOrEditQualificationModalComponent } from './create-or-edit-qualification-modal.component';
import { ViewQualificationModalComponent } from './view-qualification-modal.component';

@NgModule({
    declarations: [QualificationComponent, CreateOrEditQualificationModalComponent, ViewQualificationModalComponent],
    imports: [AppSharedModule, QualificationRoutingModule, AdminSharedModule],
})
export class QualificationModule {}
