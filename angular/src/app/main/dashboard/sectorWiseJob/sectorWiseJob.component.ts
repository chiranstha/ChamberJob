import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { JobDashboardServiceProxy } from '@shared/service-proxies/service-proxies';
import { result } from 'lodash-es';

import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexAnnotations,
  ApexFill,
  ApexStroke,
  ApexGrid
} from "ng-apexcharts";


export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: any; //ApexXAxis;
  annotations: ApexAnnotations;
  fill: ApexFill;
  stroke: ApexStroke;
  grid: ApexGrid;
};

@Component({
  selector: 'app-sectorWiseJob',
  templateUrl: './sectorWiseJob.component.html',
  styleUrls: ['./sectorWiseJob.component.css']
})
export class SectorWiseJobComponent extends AppComponentBase implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  constructor(injector: Injector,private _proxy:JobDashboardServiceProxy) {
    super(injector);
  }


  
  ngOnInit() {


    this._proxy.getCompanyTotalJobChart().subscribe(result=>{
    this.chartOptions = {
      series: [
        {
          name: "Company Job Demands",
          data: result.data
        }
      ],
   
      chart: {
        height: 350,
        type: "bar"
      },
      plotOptions: {
        bar: {
          columnWidth: "50%",
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        width: 2
      },

      grid: {
        row: {
          colors: ["#fff", "#f2f2f2"]
        }
      },
      xaxis: {
        labels: {
          rotate: -45
        },
        categories:result.category,
        tickPlacement: "on"
      },
      yaxis: {
        title: {
          text: "Company Job Demand"
        }
      },
      fill: {
        type: "gradient",
        gradient: {
          shade: "light",
          type: "horizontal",
          shadeIntensity: 0.25,
          gradientToColors: undefined,
          inverseColors: true,
          opacityFrom: 0.85,
          opacityTo: 0.85,
          stops: [50, 0, 100,150,500,1000,1500,2000,5000]
        }
      }
    };
  });
  }
  }

