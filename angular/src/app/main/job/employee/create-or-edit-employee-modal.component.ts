import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, ElementRef } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    EmployeeServiceProxy,
    CreateOrEditEmployeeDto,
    EmployeeJobSkillLookupTableDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { FileUploader, FileUploaderOptions } from '@node_modules/ng2-file-upload';
import { IAjaxResponse, TokenService } from '@node_modules/abp-ng2-module';
import { AppConsts } from '@shared/AppConsts';

import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'createOrEditEmployeeModal',
    templateUrl: './create-or-edit-employee-modal.component.html',
})
export class CreateOrEditEmployeeModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    employee: CreateOrEditEmployeeDto = new CreateOrEditEmployeeDto();

    jobSkillName = '';

    allJobSkills: EmployeeJobSkillLookupTableDto[];

    photoFileUploader: FileUploader;
    photoFileToken: string;
    photoFileName: string;
    photoFileAcceptedTypes: string = '';
    @ViewChild('Employee_photoLabel') employee_photoLabel: ElementRef;

    constructor(
        injector: Injector,
        private _employeeServiceProxy: EmployeeServiceProxy,
        private _dateTimeService: DateTimeService,
        private _tokenService: TokenService,
        private _http: HttpClient
    ) {
        super(injector);
    }

    show(employeeId?: string): void {
        if (!employeeId) {
            this.employee = new CreateOrEditEmployeeDto();
            this.employee.id = employeeId;
            this.employee.dbo = this._dateTimeService.getStartOfDay();
            this.jobSkillName = '';

            this.photoFileName = null;

            this.active = true;
            this.modal.show();
        } else {
            this._employeeServiceProxy.getEmployeeForEdit(employeeId).subscribe((result) => {
                this.employee = result;
                this.active = true;
                this.modal.show();
            });
        }
        this._employeeServiceProxy.getAllJobSkillForTableDropdown().subscribe((result) => {
            this.allJobSkills = result;
        });

        this.photoFileUploader = this.initializeUploader(
            AppConsts.remoteServiceBaseUrl + '/Employee/UploadphotoFile',
            (fileToken) => (this.photoFileToken = fileToken)
        );
    }

    save(): void {
        this.saving = true;

        this.employee.photoToken = this.photoFileToken;

        this._employeeServiceProxy
            .createOrEdit(this.employee)
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

    onSelectPhotoFile(fileInput: any): void {
        let selectedFile = <File>fileInput.target.files[0];

        this.photoFileUploader.clearQueue();
        this.photoFileUploader.addToQueue([selectedFile]);
        this.photoFileUploader.uploadAll();
    }

    removePhotoFile(): void {
        this.message.confirm(this.l('DoYouWantToRemoveTheFile'), this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._employeeServiceProxy.removePhotoFile(this.employee.id).subscribe(() => {
                    abp.notify.success(this.l('SuccessfullyDeleted'));
                    this.photoFileName = null;
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
        this._http.get(AppConsts.remoteServiceBaseUrl + '/employee/GetPhotoFileAllowedTypes').subscribe((data: any) => {
            if (!data || !data.result) {
                return;
            }

            let list = data.result as string[];
            if (list.length == 0) {
                return;
            }

            for (let i = 0; i < list.length; i++) {
                this.photoFileAcceptedTypes += '.' + list[i] + ',';
            }
        });
    }
}
