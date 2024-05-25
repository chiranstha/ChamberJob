import { Component, ViewChild, Injector, Output, EventEmitter, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import {
    JobDemandsServiceProxy,
    JobDemandCompanyLookupTableDto,
    JobDemandJobSkillLookupTableDto,
} from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import NepaliDate from 'nepali-date-converter';
import { DateTimeService } from '@app/shared/common/timing/date-time.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'createOrEditJobDemandModal',
    templateUrl: './create-or-edit-jobDemand-modal.component.html',
})
export class CreateOrEditJobDemandModalComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    id: string;



    companyName = '';
    jobSkillName = '';
    form: FormGroup;

    allCompanys: JobDemandCompanyLookupTableDto[];
    allJobSkills: JobDemandJobSkillLookupTableDto[];

    constructor(
        injector: Injector,
        private _jobDemandsServiceProxy: JobDemandsServiceProxy,
        private _dateTimeService: DateTimeService,
        private fb: FormBuilder,
    ) {
        super(injector);
    }


    ngOnInit(): void {
        this.createForm();
        this.getAllCompany();
        this.getAllJobskill();
    }


    createForm(item: any = {}) {

        let date = new NepaliDate(new Date());
        let today = date.format('YYYY/MM/DD');

        let expireddate = new NepaliDate(new Date());
        let getexpireddate = date.format('YYYY/MM/DD');
        this.form = this.fb.group({
            name: [item.name ? item.name : '', Validators.required],
            address: [item.address ? item.address : '', Validators.required],
            dateMiti: [item.dateMiti ? item.dateMiti : today, Validators.required],
            salary: [item.salary ? item.salary : '', Validators.required],
            interviewDateMiti: [item.interviewDateMiti ? item.interviewDateMiti : getexpireddate, Validators.required],
            experienceLevel: [item.experienceLevel ? item.experienceLevel : '', Validators.required],
            expiredDateMiti: [item.expiredDateMiti ? item.expiredDateMiti : getexpireddate, Validators.required],
            jobSpecification: [item.jobSpecification ? item.jobSpecification : '', Validators.required],
            description: [item.description ? item.description : ''],
            companyId: [item.companyId ? item.companyId : '', Validators.required],
            jobSkillId: [item.jobSkillId ? item.jobSkillId : null, Validators.required],
            id: [item.id ? item.id : null],
        });
    }

    show(jobDemandId?: string): void {
        this.createForm();
        this.getAllCompany();
        this.getAllJobskill();
        this.id = jobDemandId;
        if (jobDemandId) {
            this._jobDemandsServiceProxy.getJobDemandForEdit(jobDemandId).subscribe((result) => {
                this.createForm(result);
            });
        }
        this.active = true;
        this.modal.show();

    }


    getAllCompany() {
        this._jobDemandsServiceProxy.getAllCompanyForTableDropdown().subscribe((result) => {
            this.allCompanys = result;
        });
    }

    getAllJobskill() {
        this._jobDemandsServiceProxy.getAllJobSkillForTableDropdown().subscribe((result) => {
            this.allJobSkills = result;
        });
    }


    save(): void {
        this.saving = true;

        this._jobDemandsServiceProxy
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

    close(): void {
        this.active = false;
        this.modal.hide();
    }


}
