import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { CompanyCategoryRoutingModule } from './companyCategory-routing.module';
import { CompanyCategoryComponent } from './companyCategory.component';
import { CreateOrEditCompanyCategoryModalComponent } from './create-or-edit-companyCategory-modal.component';
import { ViewCompanyCategoryModalComponent } from './view-companyCategory-modal.component';

@NgModule({
    declarations: [
        CompanyCategoryComponent,
        CreateOrEditCompanyCategoryModalComponent,
        ViewCompanyCategoryModalComponent,
    ],
    imports: [AppSharedModule, CompanyCategoryRoutingModule, AdminSharedModule],
})
export class CompanyCategoryModule {}
