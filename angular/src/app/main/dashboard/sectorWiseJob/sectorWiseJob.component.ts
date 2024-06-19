import { Component, Injector, OnInit, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

import {
  ChartComponent,
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexStroke,
  ApexYAxis,
  ApexFill,
  ApexLegend,
  ApexPlotOptions
} from "ng-apexcharts";


export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  stroke: ApexStroke;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  colors: string[];
  fill: ApexFill;
  legend: ApexLegend;
};

@Component({
  selector: 'app-sectorWiseJob',
  templateUrl: './sectorWiseJob.component.html',
  styleUrls: ['./sectorWiseJob.component.css']
})
export class SectorWiseJobComponent extends AppComponentBase implements OnInit {
  @ViewChild("chart") chart: ChartComponent;
  public chartOptions: Partial<ChartOptions>;
  constructor(injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    this.chartOptions = {
      series: [
        {
          name: "Q1 Budget",
          group: "budget",
          data: [44000, 55000, 41000, 67000, 22000, 43000]
        },
        {
          name: "Q1 Actual",
          group: "actual",
          data: [48000, 50000, 40000, 65000, 25000, 40000]
        },
        {
          name: "Q2 Budget",
          group: "budget",
          data: [13000, 36000, 20000, 8000, 13000, 27000]
        },
        {
          name: "Q2 Actual",
          group: "actual",
          data: [20000, 40000, 25000, 10000, 12000, 28000]
        }
      ],
      chart: {
        type: "bar",
        height: 350,
        stacked: true
      },
      stroke: {
        width: 1,
        colors: ["#fff"]
      },
      dataLabels: {
        formatter: (val) => {
          return Number(val) / 1000 + "K";
        }
      },
      plotOptions: {
        bar: {
          horizontal: false
        }
      },
      xaxis: {
        categories: [
          "Online advertising",
          "Sales Training",
          "Print advertising",
          "Catalogs",
          "Meetings",
          "Public relations"
        ]
      },
      fill: {
        opacity: 1
      },
      colors: ["#80c7fd", "#008FFB", "#80f1cb", "#00E396"],
      yaxis: {
        labels: {
          formatter: (val) => {
            return val / 1000 + "K";
          }
        }
      },
      legend: {
        position: "top",
        horizontalAlign: "left"
      }
    };
  }

}
