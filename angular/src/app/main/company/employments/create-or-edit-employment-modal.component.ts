import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    EmploymentsServiceProxy,
    CreateOrEditEmploymentDto,
    EmploymentCompanyLookupTableDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditEmploymentModal',
    templateUrl: './create-or-edit-employment-modal.component.html',
})
export class CreateOrEditEmploymentModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    employment: CreateOrEditEmploymentDto = new CreateOrEditEmploymentDto();

    companyName = '';

    allCompanys: EmploymentCompanyLookupTableDto[];

    constructor(
        injector: Injector,
        private _employmentsServiceProxy: EmploymentsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(employmentId?: string): void {
        if (!employmentId) {
            this.employment = new CreateOrEditEmploymentDto();
            this.employment.id = employmentId;
            this.companyName = '';

            this.active = true;
            this.modal.show();
        } else {
            this._employmentsServiceProxy.getEmploymentForEdit(employmentId).subscribe((result) => {
                this.employment = result;
                this.active = true;
                this.modal.show();
            });
        }
        this._employmentsServiceProxy.getAllCompanyForTableDropdown().subscribe((result) => {
            this.allCompanys = result;
        });
    }

    save(): void {
        this.saving = true;

        this._employmentsServiceProxy
            .createOrEdit(this.employment)
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
