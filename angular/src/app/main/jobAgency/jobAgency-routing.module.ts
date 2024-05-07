import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CompanyComponent } from './company/company.component';
import { EmployeeComponent } from './employee/employee.component';
import { EmploymentsComponent } from './employments/employments.component';
import { JobdemandComponent } from './jobdemand/jobdemand.component';



@NgModule({
    imports: [RouterModule.forChild([
        {
            path: '',
            children: [
                { path: 'jobdemand', component: JobdemandComponent, data: {  } },
                { path: 'company', component: CompanyComponent, data: {  } },
                { path: 'employee', component: EmployeeComponent, data: {  } },
                { path: 'employments', component: EmploymentsComponent, data: {  } },

            ]
        }
    ])],
    exports: [RouterModule],
})
export class JobAgencyRoutingModule { }
