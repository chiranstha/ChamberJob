﻿import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JobApplyServiceProxy, JobApplyDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditJobApplyModalComponent } from './create-or-edit-jobApply-modal.component';

import { ViewJobApplyModalComponent } from './view-jobApply-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './jobApply.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class JobApplyComponent extends AppComponentBase {
    @ViewChild('createOrEditJobApplyModal', { static: true })
    createOrEditJobApplyModal: CreateOrEditJobApplyModalComponent;
    @ViewChild('viewJobApplyModal', { static: true }) viewJobApplyModal: ViewJobApplyModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    companyNameFilter = '';
    jobDemandNameFilter = '';
    employeeNameFilter = '';

    constructor(
        injector: Injector,
        private _jobApplyServiceProxy: JobApplyServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getJobApply(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._jobApplyServiceProxy
            .getAll(
                this.filterText,
                this.companyNameFilter,
                this.jobDemandNameFilter,
                this.employeeNameFilter,
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

    createJobApply(): void {
        this.createOrEditJobApplyModal.show();
    }

    deleteJobApply(jobApply: JobApplyDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._jobApplyServiceProxy.delete(jobApply.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    getDownloadUrl(id: string): string {
        return AppConsts.remoteServiceBaseUrl + '/File/DownloadBinaryFile?id=' + id;
    }

    resetFilters(): void {
        this.filterText = '';
        this.companyNameFilter = '';
        this.jobDemandNameFilter = '';
        this.employeeNameFilter = '';

        this.getJobApply();
    }
}
