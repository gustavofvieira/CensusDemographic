// import { Component, Input, AfterViewInit, OnDestroy, ElementRef } from '@angular/core';
import { Component, Input, OnInit, TemplateRef, AfterViewInit, OnDestroy, ElementRef, Inject, LOCALE_ID } from '@angular/core';
import * as Highcharts from 'highcharts';

// @Component({
//   selector: 'app-chart',
//   template: '<div [id]="chartId"></div>'
// })
// export class ChartComponent implements AfterViewInit, OnDestroy {
    
//   @Input() chartId: string;
//   @Input() chartData: Highcharts.Options;

//   private chart: Highcharts.Chart;

//   constructor(private elementRef: ElementRef) {}

//   ngAfterViewInit() {
//     this.chart = Highcharts.chart(this.elementRef.nativeElement.querySelector(`#${this.chartId}`), this.chartData);
//   }

//   ngOnDestroy() {
//     this.chart.destroy();
//   }
// }
@Component({
    selector: 'app-chart',
    templateUrl: './chart.component.html',
    styleUrls: ['./census.component.css']
  })

export class ChartComponent implements AfterViewInit, OnDestroy {

 constructor(@Inject(LOCALE_ID) public locale: string,
  private chart: Highcharts.Chart,
  // private chartData: Highcharts.Options,
   private elementRef: ElementRef
   ) {}

 ngOnInit(): void {
  
  }

  // @Input() chartData: Highcharts.Options;
//   chartData: Highcharts.Options;
  chartId: string | undefined;

  ngAfterViewInit() {
    // this.chart = Highcharts.chart(this.elementRef.nativeElement.querySelector(`#${this.chartId}`), this.chartData);
  }

  ngOnDestroy() {
    this.chart.destroy();
  }
 
}
