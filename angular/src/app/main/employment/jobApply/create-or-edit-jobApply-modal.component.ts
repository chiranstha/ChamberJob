import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    JobApplyServiceProxy,
    CreateOrEditJobApplyDto,
    JobApplyCompanyLookupTableDto,
    JobApplyJobDemandLookupTableDto,
    JobApplyEmployeeLookupTableDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { FileUploader, FileUploaderOptions } from '@node_modules/ng2-file-upload';
import { IAjaxResponse, TokenService } from '@node_modules/abp-ng2-module';
import { AppConsts } from '@shared/AppConsts';

import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'createOrEditJobApplyModal',
    templateUrl: './create-or-edit-jobApply-modal.component.html',
})
export class CreateOrEditJobApplyModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    jobApply: CreateOrEditJobApplyDto = new CreateOrEditJobApplyDto();

    companyName = '';
    jobDemandName = '';
    employeeName = '';

    allCompanys: JobApplyCompanyLookupTableDto[];
    allJobDemands: JobApplyJobDemandLookupTableDto[];
    allEmployees: JobApplyEmployeeLookupTableDto[];

    documentFileUploader: FileUploader;
    documentFileToken: string;
    documentFileName: string;
    documentFileAcceptedTypes: string = '';
    @ViewChild('JobApply_documentLabel') jobApply_documentLabel: ElementRef;

    constructor(
        injector: Injector,
        private _jobApplyServiceProxy: JobApplyServiceProxy,
        private _dateTimeService: DateTimeService,
        private _tokenService: TokenService,
        private _http: HttpClient
    ) {
        super(injector);
    }

    show(jobApplyId?: string): void {
        if (!jobApplyId) {
            this.jobApply = new CreateOrEditJobApplyDto();
            this.jobApply.id = jobApplyId;
            this.jobApply.date = this._dateTimeService.getStartOfDay();
            this.companyName = '';
            this.jobDemandName = '';
            this.employeeName = '';

            this.documentFileName = null;

            this.active = true;
            this.modal.show();
        } else {
            this._jobApplyServiceProxy.getJobApplyForEdit(jobApplyId).subscribe((result) => {
                this.jobApply = result;

               
                this.active = true;
                this.modal.show();
            });
        }
        this._jobApplyServiceProxy.getAllCompanyForTableDropdown().subscribe((result) => {
            this.allCompanys = result;
        });
        this._jobApplyServiceProxy.getAllJobDemandForTableDropdown().subscribe((result) => {
            this.allJobDemands = result;
        });
        this._jobApplyServiceProxy.getAllEmployeeForTableDropdown().subscribe((result) => {
            this.allEmployees = result;
        });

        this.documentFileUploader = this.initializeUploader(
            AppConsts.remoteServiceBaseUrl + '/JobApply/UploaddocumentFile',
            (fileToken) => (this.documentFileToken = fileToken)
        );
    }

    save(): void {
        this.saving = true;

        this.jobApply.documentToken = this.documentFileToken;

        this._jobApplyServiceProxy
            .createOrEdit(this.jobApply)
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

    onSelectDocumentFile(fileInput: any): void {
        let selectedFile = <File>fileInput.target.files[0];

        this.documentFileUploader.clearQueue();
        this.documentFileUploader.addToQueue([selectedFile]);
        this.documentFileUploader.uploadAll();
    }

    removeDocumentFile(): void {
        this.message.confirm(this.l('DoYouWantToRemoveTheFile'), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._jobApplyServiceProxy.removeDocumentFile(this.jobApply.id).subscribe(() => {
                    abp.notify.success(this.l('SuccessfullyDeleted'));
                    this.documentFileName = null;
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
        this._http
            .get(AppConsts.remoteServiceBaseUrl + '/jobApply/GetDocumentFileAllowedTypes')
            .subscribe((data: any) => {
                if (!data || !data.result) {
                    return;
                }

                let list = data.result as string[];
                if (list.length == 0) {
                    return;
                }

                for (let i = 0; i < list.length; i++) {
                    this.documentFileAcceptedTypes += '.' + list[i] + ',';
                }
            });
    }
}
