import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-companyWiseJob',
  templateUrl: './companyWiseJob.component.html',
  styleUrls: ['./companyWiseJob.component.css']
})
export class CompanyWiseJobComponent extends AppComponentBase implements OnInit {

  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit() {
  }

}
