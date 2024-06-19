import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CreateOrEditJobDemandModalComponent } from '@app/main/job/jobDemands/create-or-edit-jobDemand-modal.component';
import { ViewJobDemandModalComponent } from '@app/main/job/jobDemands/view-jobDemand-modal.component';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { EmployeeServiceProxy, ExperienceLevelEnum, JobApplyServiceProxy, JobDemandsServiceProxy, TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { NotifyService } from 'abp-ng2-module';
import { LazyLoadEvent } from 'primeng/api';
import { Paginator } from 'primeng/paginator';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-jobdemand',
  templateUrl: './jobdemand.component.html',
  styleUrls: ['./jobdemand.component.css']
})
export class JobdemandComponent extends AppComponentBase {
  @ViewChild('createOrEditJobDemandModal', { static: true })
  createOrEditJobDemandModal: CreateOrEditJobDemandModalComponent;
  @ViewChild('viewJobDemandModal', { static: true }) viewJobDemandModal: ViewJobDemandModalComponent;

  @ViewChild('dataTable', { static: true }) dataTable: Table;
  @ViewChild('paginator', { static: true }) paginator: Paginator;

  advancedFiltersAreShown = false;
  filterText = '';
  companyNameFilter = '';
  jobSkillNameFilter = '';

  experienceLevelEnum = ExperienceLevelEnum;

  constructor(
      injector: Injector,
      private _jobDemandsServiceProxy: JobApplyServiceProxy,
      private _notifyService: NotifyService,
      private _tokenAuth: TokenAuthServiceProxy,
      private _activatedRoute: ActivatedRoute,
      private _fileDownloadService: FileDownloadService,
      private _dateTimeService: DateTimeService
  ) {
      super(injector);
  }

  getJobDemands(event?: LazyLoadEvent) {
      if (this.primengTableHelper.shouldResetPaging(event)) {
          this.paginator.changePage(0);
          if (this.primengTableHelper.records && this.primengTableHelper.records.length > 0) {
              return;
          }
      }

      this.primengTableHelper.showLoadingIndicator();

      this._jobDemandsServiceProxy
          .getAllJobDemand(
              this.filterText,
              this.companyNameFilter,
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

  createJobDemand(): void {
      this.createOrEditJobDemandModal.show();
  }

  deleteJobDemand(id: string): void {
      this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
          if (isConfirmed) {
              this._jobDemandsServiceProxy.delete(id).subscribe(() => {
                  this.reloadPage();
                  this.notify.success(this.l('SuccessfullyDeleted'));
              });
          }
      });
  }

  
  resetFilters(): void {
      this.filterText = '';
      this.companyNameFilter = '';
      this.jobSkillNameFilter = '';

      this.getJobDemands();
  }
}
