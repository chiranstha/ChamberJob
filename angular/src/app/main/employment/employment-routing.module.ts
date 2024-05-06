import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { JobdemandComponent } from './jobdemand/jobdemand.component';



@NgModule({
    imports: [RouterModule.forChild([
        {
            path: '',
            children: [
                { path: 'jobdemand', component: JobdemandComponent, data: {  } },
            ]
        }
    ])],
    exports: [RouterModule],
})
export class EmploymentRoutingModule { }
