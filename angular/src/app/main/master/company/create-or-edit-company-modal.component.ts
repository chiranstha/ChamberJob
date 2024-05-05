import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    CompanyServiceProxy,
    CreateOrEditCompanyDto,
    CompanyCompanyCategoryLookupTableDto,
    CompanyCompanyTypeLookupTableDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { FileUploader, FileUploaderOptions } from '@node_modules/ng2-file-upload';
import { IAjaxResponse, TokenService } from '@node_modules/abp-ng2-module';
import { AppConsts } from '@shared/AppConsts';

import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'createOrEditCompanyModal',
    templateUrl: './create-or-edit-company-modal.component.html',
})
export class CreateOrEditCompanyModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    company: CreateOrEditCompanyDto = new CreateOrEditCompanyDto();

    companyCategoryName = '';
    companyTypeName = '';

    allCompanyCategorys: CompanyCompanyCategoryLookupTableDto[];
    allCompanyTypes: CompanyCompanyTypeLookupTableDto[];

    logoFileUploader: FileUploader;
    logoFileToken: string;
    logoFileName: string;
    logoFileAcceptedTypes: string = '';
    @ViewChild('Company_logoLabel') company_logoLabel: ElementRef;

    constructor(
        injector: Injector,
        private _companyServiceProxy: CompanyServiceProxy,
        private _dateTimeService: DateTimeService,
        private _tokenService: TokenService,
        private _http: HttpClient
    ) {
        super(injector);
    }

    show(companyId?: number): void {
        if (!companyId) {
            this.company = new CreateOrEditCompanyDto();
            this.company.id = companyId;
            this.companyCategoryName = '';
            this.companyTypeName = '';

            this.logoFileName = null;

            this.active = true;
            this.modal.show();
        } else {
            this._companyServiceProxy.getCompanyForEdit(companyId).subscribe((result) => {
                this.company = result.company;

                this.companyCategoryName = result.companyCategoryName;
                this.companyTypeName = result.companyTypeName;

                this.logoFileName = result.logoFileName;

                this.active = true;
                this.modal.show();
            });
        }
        this._companyServiceProxy.getAllCompanyCategoryForTableDropdown().subscribe((result) => {
            this.allCompanyCategorys = result;
        });
        this._companyServiceProxy.getAllCompanyTypeForTableDropdown().subscribe((result) => {
            this.allCompanyTypes = result;
        });

        this.logoFileUploader = this.initializeUploader(
            AppConsts.remoteServiceBaseUrl + '/Company/UploadlogoFile',
            (fileToken) => (this.logoFileToken = fileToken)
        );
    }

    save(): void {
        this.saving = true;

        this.company.logoToken = this.logoFileToken;

        this._companyServiceProxy
            .createOrEdit(this.company)
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

    onSelectLogoFile(fileInput: any): void {
        let selectedFile = <File>fileInput.target.files[0];

        this.logoFileUploader.clearQueue();
        this.logoFileUploader.addToQueue([selectedFile]);
        this.logoFileUploader.uploadAll();
    }

    removeLogoFile(): void {
        this.message.confirm(this.l('DoYouWantToRemoveTheFile'), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._companyServiceProxy.removeLogoFile(this.company.id).subscribe(() => {
                    abp.notify.success(this.l('SuccessfullyDeleted'));
                    this.logoFileName = null;
                });
            }
        });
    }

    initializeUploader(url: string, onSuccess: (fileToken: string) => void): FileUploader {
        let uploader = new FileUploader({ url: url });

        let _uploaderOptions: FileUploaderOptions = {};
        _uploaderOptions.autoUpload = false;
        _uploaderOptions.authToken = 'Bearer ' + this._tokenService.getToken();
        _uploaderOptions.removeAfterUpload = true;

        uploader.onAfterAddingFile = (file) => {
            file.withCredentials = false;
        };

        uploader.onSuccessItem = (item, response, status) => {
            const resp = <IAjaxResponse>JSON.parse(response);
            if (resp.success && resp.result.fileToken) {
                onSuccess(resp.result.fileToken);
            } else {
                this.message.error(resp.result.message);
            }
        };

        uploader.setOptions(_uploaderOptions);
        return uploader;
    }

    getDownloadUrl(id: string): string {
        return AppConsts.remoteServiceBaseUrl + '/File/DownloadBinaryFile?id=' + id;
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    ngOnInit(): void {
        this._http.get(AppConsts.remoteServiceBaseUrl + '/company/GetLogoFileAllowedTypes').subscribe((data: any) => {
            if (!data || !data.result) {
                return;
            }

            let list = data.result as string[];
            if (list.length == 0) {
                return;
            }

            for (let i = 0; i < list.length; i++) {
                this.logoFileAcceptedTypes += '.' + list[i] + ',';
            }
        });
    }
}
