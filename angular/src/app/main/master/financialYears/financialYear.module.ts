import { NgModule } from '@angular/core';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { FinancialYearRoutingModule } from './financialYear-routing.module';
import { FinancialYearsComponent } from './financialYears.component';
import { CreateOrEditFinancialYearModalComponent } from './create-or-edit-financialYear-modal.component';
import { ViewFinancialYearModalComponent } from './view-financialYear-modal.component';

@NgModule({
    declarations: [FinancialYearsComponent, CreateOrEditFinancialYearModalComponent, ViewFinancialYearModalComponent],
    imports: [AppSharedModule, FinancialYearRoutingModule, AdminSharedModule],
})
export class FinancialYearModule {}
