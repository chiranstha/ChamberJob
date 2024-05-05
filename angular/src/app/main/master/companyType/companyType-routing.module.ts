import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyTypeComponent } from './companyType.component';

const routes: Routes = [
    {
        path: '',
        component: CompanyTypeComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class CompanyTypeRoutingModule {}
