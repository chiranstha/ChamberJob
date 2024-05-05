import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmploymentsComponent } from './employments.component';

const routes: Routes = [
    {
        path: '',
        component: EmploymentsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EmploymentRoutingModule {}
