﻿import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyServiceProxy, CompanyDto, BusinessNatureEnum } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditCompanyModalComponent } from './create-or-edit-company-modal.component';

import { ViewCompanyModalComponent } from './view-company-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './company.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class CompanyComponent extends AppComponentBase {
    @ViewChild('createOrEditCompanyModal', { static: true })
    createOrEditCompanyModal: CreateOrEditCompanyModalComponent;
    @ViewChild('viewCompanyModal', { static: true }) viewCompanyModal: ViewCompanyModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    companyCategoryNameFilter = '';
    companyTypeNameFilter = '';

    businessNatureEnum = BusinessNatureEnum;

    constructor(
        injector: Injector,
        private _companyServiceProxy: CompanyServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    getCompany(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
                return;
            }
        }

        this.primengTableHelper.showLoadingIndicator();

        this._companyServiceProxy
            .getAll(
                this.filterText,
                this.companyCategoryNameFilter,
                this.companyTypeNameFilter,
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

    createCompany(): void {
        this.createOrEditCompanyModal.show();
    }

    deleteCompany(company: CompanyDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._companyServiceProxy.delete(company.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._companyServiceProxy
            .getCompanyToExcel(this.filterText, this.companyCategoryNameFilter, this.companyTypeNameFilter)
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    getDownloadUrl(id: string): string {
        return AppConsts.remoteServiceBaseUrl + '/File/DownloadBinaryFile?id=' + id;
    }

    resetFilters(): void {
        this.filterText = '';
        this.companyCategoryNameFilter = '';
        this.companyTypeNameFilter = '';

        this.getCompany();
    }
}