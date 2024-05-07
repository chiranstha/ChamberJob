import { Component, Injector, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DashboardCustomizationConst } from '@app/shared/common/customizable-dashboard/DashboardCustomizationConsts';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { CreateOrEditEmployeeModalComponent } from './createEmployeeModal/create-or-edit-employee-modal.component';

@Component({
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.less'],
    encapsulation: ViewEncapsulation.None,
})
export class DashboardComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditEmployeeModal', { static: true })
    createOrEditEmployeeModal: CreateOrEditEmployeeModalComponent;
    dashboardName = DashboardCustomizationConst.dashboardNames.defaultTenantDashboard;

    constructor(injector: Injector) {
        super(injector);
    }
    ngOnInit(): void {
      
    }

    openCreateOrEditModal()
    {
        this.createOrEditEmployeeModal.show();
    }
}
