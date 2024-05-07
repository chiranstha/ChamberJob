import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { JobdemandComponent } from './jobdemand/jobdemand.component';
import { EmployeeComponent } from './employee/employee.component';



@NgModule({
    imports: [RouterModule.forChild([
        {
            path: '',
            children: [
                { path: 'jobdemand', component: JobdemandComponent, data: {  } },
                { path: 'employee', component: EmployeeComponent, data: {  } },
                { path: 'jobapply', component: EmployeeComponent, data: {  } },
            ]
        }
    ])],
    exports: [RouterModule],
})
export class EmploymentRoutingModule { }
