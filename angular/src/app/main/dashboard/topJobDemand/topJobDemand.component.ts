import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { GetCompanyWiseJobChartDto, JobDashboardServiceProxy } from '@shared/service-proxies/service-proxies';
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexXAxis,
  ApexPlotOptions,
  ApexStroke,
  ApexTitleSubtitle,
  ApexYAxis,
  ApexTooltip,
  ApexFill,
  ApexLegend
} from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  stroke: ApexStroke;
  title: ApexTitleSubtitle;
  tooltip: ApexTooltip;
  fill: ApexFill;
  legend: ApexLegend;
};

@Component({
  selector: 'app-topJobDemand',
  templateUrl: './topJobDemand.component.html',
  styleUrls: ['./topJobDemand.component.css']
})
export class TopJobDemandComponent extends AppComponentBase implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  getJobChart: GetCompanyWiseJobChartDto=new GetCompanyWiseJobChartDto();
  constructor(injector: Injector,private _proxy: JobDashboardServiceProxy) {
    super(injector);
  }


  getAlljobDemandSeries()
  {
    this._proxy.getCompanyWiseJobChart().subscribe(result=>{ 
      this.getJobChart=result;



      this.chartOptions = {
        series: this.getJobChart.series,
        chart: {
          type: "bar",
          height: 950,
          stacked: true
        },
        plotOptions: {
          bar: {
            horizontal: true
          }
        },
        stroke: {
          width: 1,
          colors: ["#fff"]
        },
        title: {
          text: "Job Sector Wise Company"
        },
        xaxis: {
          categories:  this.getJobChart.category,
          labels: {
            formatter: function(val) {
              return val + " Demand";
            }
          }
        },
        yaxis: {
          title: {
            text: undefined
          }
        },
        tooltip: {
          y: {
            formatter: function(val) {
              return val + " Required";
            }
          }
        },
        fill: {
          opacity: 1
        },
        legend: {
          position: "top",
          horizontalAlign: "left",
          offsetX: 40
        }
      };
     });
  }

  ngOnInit() {

    this.getAlljobDemandSeries();

    

   
  }

}
