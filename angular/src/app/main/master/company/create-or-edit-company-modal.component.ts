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
import NepaliDate from 'nepali-date-converter';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'createOrEditCompanyModal',
    templateUrl: './create-or-edit-company-modal.component.html',
})
export class CreateOrEditCompanyModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @ViewChild('Company_logoLabel') company_logoLabel: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    form: FormGroup;
    active = false;
    saving = false;
    companyCategoryName = '';
    companyTypeName = '';

    allCompanyCategorys: CompanyCompanyCategoryLookupTableDto[];
    allCompanyTypes: CompanyCompanyTypeLookupTableDto[];

    logoFileUploader: FileUploader;
    logoFileToken: string;
    logoFileName: string;
    logoFileAcceptedTypes = '';

    id: number;

    constructor(
        injector: Injector,
        private _companyServiceProxy: CompanyServiceProxy,
        private _dateTimeService: DateTimeService,
        private _tokenService: TokenService,
        private _http: HttpClient,
        private fb: FormBuilder,
    ) {
        super(injector);
    }


    ngOnInit(): void {
        this.createForm();
        this.getAllCompanyCategory();
        this.getAllCompanyType();
        this._http.get(AppConsts.remoteServiceBaseUrl + '/company/GetLogoFileAllowedTypes').subscribe((data: any) => {
            if (!data || !data.result) {
                return;
            }

            let list = data.result as string[];
            if (list.length === 0) {
                return;
            }

            for (let i = 0; i < list.length; i++) {
                this.logoFileAcceptedTypes += '.' + list[i] + ',';
            }
        });
    }





    createForm(item: any = {}) {
        this.form = this.fb.group({
            name: [item.name ? item.name : '', Validators.required],
            address: [item.address ? item.address : '', Validators.required],
            authorizedPerson: [item.authorizedPerson ? item.authorizedPerson : ''],
            contactNo: [item.contactNo ? item.contactNo : ''],
            businessNature: [item.businessNature ? item.businessNature : ''],
            establishedYear: [item.establishedYear ? item.establishedYear : ''],
            website: [item.website ? item.website : ''],
            vatNo: [item.vatNo ? item.vatNo : ''],
            logo: [item.logo ? item.logo : ''],
            logoToken: [item.logoToken ? item.logoToken : ''],
            companyCategoryId: [item.companyCategoryId ? item.companyCategoryId : '', Validators.required],
            companyTypeId: [item.companyTypeId ? item.companyTypeId : '', Validators.required],
            id: [item.id ? item.id : null],
        });
    }


    show(companyId?: number): void {
        this.id = companyId;
        if (companyId) {

            this._companyServiceProxy.getCompanyForEdit(companyId).subscribe((result) => {
                this.createForm(result);


                this.logoFileName = result.logoFileName;


            });
        }



        this.active = true;
        this.modal.show();


        this.logoFileUploader = this.initializeUploader(
            AppConsts.remoteServiceBaseUrl + '/Company/UploadlogoFile',
            (fileToken) => (this.logoFileToken = fileToken)
        );
    }


    getAllCompanyCategory() {
        this._companyServiceProxy.getAllCompanyCategoryForTableDropdown().subscribe((result) => {
            this.allCompanyCategorys = result;
        });
        ;
    }

    getAllCompanyType() {

        this._companyServiceProxy.getAllCompanyTypeForTableDropdown().subscribe((result) => {
            this.allCompanyTypes = result;
        });
    }



    save(): void {
        this.saving = true;

        this.form.get('logoToken').setValue( this.logoFileToken);

        this._companyServiceProxy
            .createOrEdit(this.form.getRawValue())
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
                let companyId = this.form.get('id').value;
                this._companyServiceProxy.removeLogoFile(companyId).subscribe(() => {
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


}
