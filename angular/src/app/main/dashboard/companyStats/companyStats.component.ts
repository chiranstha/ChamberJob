import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'app-companyStats',
  templateUrl: './companyStats.component.html',
  styleUrls: ['./companyStats.component.css']
})
export class CompanyStatsComponent extends AppComponentBase implements OnInit {

  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit() {
  }

}
