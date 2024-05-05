import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { JobSkillRoutingModule } from './jobSkill-routing.module';
import { JobSkillComponent } from './jobSkill.component';
import { CreateOrEditJobSkillModalComponent } from './create-or-edit-jobSkill-modal.component';
import { ViewJobSkillModalComponent } from './view-jobSkill-modal.component';

@NgModule({
    declarations: [JobSkillComponent, CreateOrEditJobSkillModalComponent, ViewJobSkillModalComponent],
    imports: [AppSharedModule, JobSkillRoutingModule, AdminSharedModule],
})
export class JobSkillModule {}
