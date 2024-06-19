import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { GetJobStatsDto, JobDashboardServiceProxy } from '@shared/service-proxies/service-proxies';
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexXAxis,
  ApexPlotOptions
} from "ng-apexcharts";


export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
};

@Component({
  selector: 'app-addressWiseJobDemand',
  templateUrl: './addressWiseJobDemand.component.html',
  styleUrls: ['./addressWiseJobDemand.component.css']
})
export class AddressWiseJobDemandComponent extends AppComponentBase implements OnInit {

  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  jobStats: GetJobStatsDto = new GetJobStatsDto();

  constructor(injector: Injector,
    private _proxy: JobDashboardServiceProxy
  ) {
    super(injector);
  }

  ngOnInit() {
    this.getAllJobStats();
  }

  getAllJobStats() {
    this._proxy.getAddressWiseJobChart().subscribe(result => {




      this.chartOptions = {
        series: [
          {
            name: "basic",
            data: result.data
          }
        ],
        chart: {
          type: "bar",
          height: 350
        },
        plotOptions: {
          bar: {
            horizontal: true
          }
        },
        dataLabels: {
          enabled: false
        },
        xaxis: {
          categories: result.category
        }
      }
    });
  }

}
