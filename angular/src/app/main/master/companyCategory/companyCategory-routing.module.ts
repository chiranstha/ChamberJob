import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompanyCategoryComponent } from './companyCategory.component';

const routes: Routes = [
    {
        path: '',
        component: CompanyCategoryComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class CompanyCategoryRoutingModule {}
