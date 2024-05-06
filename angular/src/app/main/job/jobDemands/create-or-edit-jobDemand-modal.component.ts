import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    JobDemandsServiceProxy,
    CreateOrEditJobDemandDto,
    JobDemandCompanyLookupTableDto,
    JobDemandJobSkillLookupTableDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditJobDemandModal',
    templateUrl: './create-or-edit-jobDemand-modal.component.html',
})
export class CreateOrEditJobDemandModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    jobDemand: CreateOrEditJobDemandDto = new CreateOrEditJobDemandDto();

    companyName = '';
    jobSkillName = '';

    allCompanys: JobDemandCompanyLookupTableDto[];
    allJobSkills: JobDemandJobSkillLookupTableDto[];

    constructor(
        injector: Injector,
        private _jobDemandsServiceProxy: JobDemandsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(jobDemandId?: string): void {
        if (!jobDemandId) {
            this.jobDemand = new CreateOrEditJobDemandDto();
            this.jobDemand.id = jobDemandId;
            this.jobDemand.date = this._dateTimeService.getStartOfDay();
            this.jobDemand.interviewDate = this._dateTimeService.getStartOfDay();
            this.jobDemand.expiredDate = this._dateTimeService.getStartOfDay();
            this.companyName = '';
            this.jobSkillName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._jobDemandsServiceProxy.getJobDemandForEdit(jobDemandId).subscribe((result) => {
                this.jobDemand = result;


                this.active = true;
                this.modal.show();
            });
        }
        this._jobDemandsServiceProxy.getAllCompanyForTableDropdown().subscribe((result) => {
            this.allCompanys = result;
        });
        this._jobDemandsServiceProxy.getAllJobSkillForTableDropdown().subscribe((result) => {
            this.allJobSkills = result;
        });
    }

    save(): void {
        this.saving = true;

        this._jobDemandsServiceProxy
            .createOrEdit(this.jobDemand)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {}
}
