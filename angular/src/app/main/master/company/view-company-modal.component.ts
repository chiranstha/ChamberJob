import { AppConsts } from '@shared/AppConsts';
import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GetCompanyForViewDto, BusinessNatureEnum } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewCompanyModal',
    templateUrl: './view-company-modal.component.html',
})
export class ViewCompanyModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetCompanyForViewDto;
    businessNatureEnum = BusinessNatureEnum;

    constructor(injector: Injector) {
        super(injector);
        this.item = new GetCompanyForViewDto();
    }

    show(item: GetCompanyForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    getDownloadUrl(id: string): string {
        return AppConsts.remoteServiceBaseUrl + '/File/DownloadBinaryFile?id=' + id;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
