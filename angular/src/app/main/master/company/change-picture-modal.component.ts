import {Component, ElementRef, EventEmitter, Injector, Output, ViewChild} from '@angular/core';
import {ModalDirective} from 'ngx-bootstrap/modal';
import {AppComponentBase} from '@shared/common/app-component-base';
import {FileItem, FileUploader, FileUploaderOptions} from 'ng2-file-upload';
import {base64ToFile, ImageCroppedEvent} from 'ngx-image-cropper';
import {IAjaxResponse, TokenService} from 'abp-ng2-module';
import {AppConsts} from '@shared/AppConsts';
import { UploadTitleService } from './uploadTitle.service';

@Component({
    selector: 'changePictureModal',
    templateUrl: './change-picture-modal.component.html'
})
export class ChangePictureModalComponent extends AppComponentBase {

    @ViewChild('changePictureModal', {static: true}) modal: ModalDirective;
    @ViewChild('uploadProfilePictureInputLabel') uploadProfilePictureInputLabel: ElementRef;

    @Output() modalSave: EventEmitter<number> = new EventEmitter<number>();

    public active = false;
    public uploader: FileUploader;
    public temporaryPictureUrl: string;
    public saving = false;
    public maxProfilPictureBytesUserFriendlyValue = 5;
    public useGravatarProfilePicture = false;
    public fileTokenString = '';
    public imageUrl: string;
    imageChangedEvent: any = '';
    userId: number = null;
    validatedMessage: string;
    uploaderTitle: string;
    aspectRatio: boolean;
    ratio: number;
    private _uploaderOptions: FileUploaderOptions = {
        url: AppConsts.remoteServiceBaseUrl + '/CampusImage/UploadImageFile'
        //for pdf url: AppConsts.remoteServiceBaseUrl + '/CampusPdf/UploadPdfFile'
    };


    constructor(injector: Injector,
                private uploadTitle: UploadTitleService,
                private _tokenService: TokenService) {
        super(injector);
    }

    initializeModal(): void {
        this.active = true;
        this.temporaryPictureUrl = '';
        this.initFileUploader();
        this.getUploadTitle();
        this.getRatio();
        this.getAspectRatio();
    }

    show(userId?: number): void {
        this.initializeModal();
        this.modal.show();
        this.imageUrl = '';
        this.imageChangedEvent = '';
        this.userId = userId;
    }

    close(): void {
        this.active = false;
        this.imageChangedEvent = '';
        this.uploader.clearQueue();
        this.modal.hide();
    }

    fileChangeEvent(event: any): void {
        const extensionType = event.target.files[0].type;
        const size = event.target.files[0].size;
        this.validatedMessage = 'You can select a JPG/JPEG/PNG file with a maximum 5MB size.';
        if ((extensionType === 'image/jpeg' || extensionType === 'image/png' || extensionType === 'image/webp' || extensionType === 'image/jpg') && size < 5000000) {
            this.imageChangedEvent = event;
        } else {
            this.validatedMessage = 'The selected file should be in JPG/JPEG/PNG format with maximum 5MB Size.';
        }

        this.uploadProfilePictureInputLabel.nativeElement.innerText = event.target.files[0].name;

        this.imageChangedEvent = event;
    }

    imageCroppedFile(event: ImageCroppedEvent) {
        this.uploader.clearQueue();
        this.uploader.addToQueue([<File>base64ToFile(event.base64)]);
        this.imageUrl = event.base64;
    }

    initFileUploader(): void {
        this.uploader = new FileUploader({url: AppConsts.remoteServiceBaseUrl + '/Profile/UploadProfilePicture'});
        this._uploaderOptions.autoUpload = false;
        this._uploaderOptions.authToken = 'Bearer ' + this._tokenService.getToken();
        this._uploaderOptions.removeAfterUpload = true;
        this.uploader.onAfterAddingFile = (file) => {
            file.withCredentials = false;
        };

        let token = this.guid();
        this.uploader.onBuildItemForm = (fileItem: FileItem, form: any) => {
            form.append('FileType', fileItem.file.type);
            form.append('FileName', 'ProfilePicture');
            form.append('FileToken', token);
        };

        this.uploader.onSuccessItem = (item, response, status) => {
            const resp = <IAjaxResponse>JSON.parse(response);
            if (resp.success) {
                this.updateProfilePicture(token);
            } else {
                this.message.error(resp.error.message);
            }
        };

        this.uploader.setOptions(this._uploaderOptions);
    }

    // when you change the picture
    updateProfilePicture(fileToken: string): void {
        this.fileTokenString = fileToken;
        abp.event.trigger('PictureChanged');
        this.modal.hide();
    }


    getUploadTitle() {
        this.uploadTitle.data.subscribe(response => {
            this.uploaderTitle = response;
        });
    }

    guid(): string {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }

        return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
    }

    save(): void {
        this.uploader.uploadAll();
    }

    getRatio() {
        this.uploadTitle.ratioData.subscribe(response => {
            this.ratio = response;
            console.log('r', this.ratio);
        });
    }

    getAspectRatio() {
        this.uploadTitle.aspectRatioData.subscribe(response => {
            this.aspectRatio = response;
            console.log('a', this.aspectRatio);
        });
    }
}
