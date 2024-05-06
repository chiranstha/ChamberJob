import { Component, Injector, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DashboardCustomizationConst } from '@app/shared/common/customizable-dashboard/DashboardCustomizationConsts';
import { CreateEmployeeModalComponent } from './createEmployeeModal/createEmployeeModal.component';

@Component({
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.less'],
    encapsulation: ViewEncapsulation.None,
})
export class DashboardComponent extends AppComponentBase implements OnInit {

    @ViewChild('CreateEmployeeModal', { static: true })
    createOrEditCompanyTypeModal: CreateEmployeeModalComponent;
    dashboardName = DashboardCustomizationConst.dashboardNames.defaultTenantDashboard;

    constructor(injector: Injector) {
        super(injector);
    }
    ngOnInit(): void {
        this.createOrEditCompanyTypeModal.show('');
    }
}
