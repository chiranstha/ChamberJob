import { Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { GetCompanyTypeForViewDto } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-createEmployeeModal',
  templateUrl: './createEmployeeModal.component.html',
  styleUrls: ['./createEmployeeModal.component.css']
})
export class CreateEmployeeModalComponent extends AppComponentBase {
  @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

  active = false;
  saving = false;

  item: GetCompanyTypeForViewDto;

  constructor(injector: Injector) {
      super(injector);
      this.item = new GetCompanyTypeForViewDto();
  }

  show(item): void {
      this.notify.error(item);
      this.active = true;
      this.modal.show();
  }

  close(): void {
      this.active = false;
      this.modal.hide();
  }
}
