import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { FinancialYearsServiceProxy, CreateOrEditFinancialYearDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditFinancialYearModal',
    templateUrl: './create-or-edit-financialYear-modal.component.html',
})
export class CreateOrEditFinancialYearModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    financialYear: CreateOrEditFinancialYearDto = new CreateOrEditFinancialYearDto();

    constructor(
        injector: Injector,
        private _financialYearsServiceProxy: FinancialYearsServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(financialYearId?: string): void {
        if (!financialYearId) {
            this.financialYear = new CreateOrEditFinancialYearDto();
            this.financialYear.id = financialYearId;

            this.active = true;
            this.modal.show();
        } else {
            this._financialYearsServiceProxy.getFinancialYearForEdit(financialYearId).subscribe((result) => {
                this.financialYear = result;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._financialYearsServiceProxy
            .createOrEdit(this.financialYear)
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
