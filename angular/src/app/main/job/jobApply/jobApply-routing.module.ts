import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JobApplyComponent } from './jobApply.component';

const routes: Routes = [
    {
        path: '',
        component: JobApplyComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class JobApplyRoutingModule {}
