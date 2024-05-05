﻿import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeServiceProxy, EmployeeDto, GenderEnum } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditEmployeeModalComponent } from './create-or-edit-employee-modal.component';

import { ViewEmployeeModalComponent } from './view-employee-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './employee.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class EmployeeComponent extends AppComponentBase {
    @ViewChild('createOrEditEmployeeModal', { static: true })
    createOrEditEmployeeModal: CreateOrEditEmployeeModalComponent;
    @ViewChild('viewEmployeeModal', { static: true }) viewEmployeeModal: ViewEmployeeModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    jobSkillNameFilter = '';

    genderEnum = GenderEnum;

    constructor(
        injector: Injector,
        private _employeeServiceProxy: EmployeeServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getEmployee(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._employeeServiceProxy
            .getAll(
                this.filterText,
                this.jobSkillNameFilter,
                this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getSkipCount(this.paginator, event),
                this.primengTableHelper.getMaxResultCount(this.paginator, event)
            )
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createEmployee(): void {
        this.createOrEditEmployeeModal.show();
    }

    deleteEmployee(employee: EmployeeDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._employeeServiceProxy.delete(employee.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._employeeServiceProxy.getEmployeeToExcel(this.filterText, this.jobSkillNameFilter).subscribe((result) => {
            this._fileDownloadService.downloadTempFile(result);
        });
    }

    getDownloadUrl(id: string): string {
        return AppConsts.remoteServiceBaseUrl + '/File/DownloadBinaryFile?id=' + id;
    }

    resetFilters(): void {
        this.filterText = '';
        this.jobSkillNameFilter = '';

        this.getEmployee();
    }
}
