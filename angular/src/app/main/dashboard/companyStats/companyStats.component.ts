import { Component, Injector, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { GetJobStatsDto, JobDashboardServiceProxy } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-companyStats',
  templateUrl: './companyStats.component.html',
  styleUrls: ['./companyStats.component.css']
})
export class CompanyStatsComponent extends AppComponentBase implements OnInit {
  jobStats: GetJobStatsDto=new GetJobStatsDto();

  constructor(injector: Injector,
    private _proxy:JobDashboardServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
    this.getAllJobStats();
  }

  getAllJobStats()
  {
    this._proxy.getJobStats().subscribe(result=>{
     this.jobStats=result;
    });
  }

}
