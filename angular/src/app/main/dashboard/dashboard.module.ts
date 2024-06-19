import { NgModule } from '@angular/core';
import { AdminSharedModule } from '@app/admin/shared/admin-shared.module';
import { AppSharedModule } from '@app/shared/app-shared.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { CustomizableDashboardModule } from '@app/shared/common/customizable-dashboard/customizable-dashboard.module';
import { NgApexchartsModule } from 'ng-apexcharts';
import { CompanyWiseJobComponent } from './companyWiseJob/companyWiseJob.component';
import { SectorWiseJobComponent } from './sectorWiseJob/sectorWiseJob.component';
import { TopJobDemandComponent } from './topJobDemand/topJobDemand.component';
import { CompanyStatsComponent } from './companyStats/companyStats.component';

@NgModule({
    declarations: [DashboardComponent,CompanyWiseJobComponent,SectorWiseJobComponent,TopJobDemandComponent,CompanyStatsComponent],
    imports: [AppSharedModule, AdminSharedModule, DashboardRoutingModule, CustomizableDashboardModule,NgApexchartsModule],
})
export class DashboardModule {}
