import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { CompanyCategoryServiceProxy, CreateOrEditCompanyCategoryDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    selector: 'createOrEditCompanyCategoryModal',
    templateUrl: './create-or-edit-companyCategory-modal.component.html',
})
export class CreateOrEditCompanyCategoryModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    companyCategory: CreateOrEditCompanyCategoryDto = new CreateOrEditCompanyCategoryDto();

    constructor(
        injector: Injector,
        private _companyCategoryServiceProxy: CompanyCategoryServiceProxy,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    show(companyCategoryId?: number): void {
        if (!companyCategoryId) {
            this.companyCategory = new CreateOrEditCompanyCategoryDto();
            this.companyCategory.id = companyCategoryId;

            this.active = true;
            this.modal.show();
        } else {
            this._companyCategoryServiceProxy.getCompanyCategoryForEdit(companyCategoryId).subscribe((result) => {
                this.companyCategory = result;

                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
        this.saving = true;

        this._companyCategoryServiceProxy
            .createOrEdit(this.companyCategory)
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
