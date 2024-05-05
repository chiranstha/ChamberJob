import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JobDemandsComponent } from './jobDemands.component';

const routes: Routes = [
    {
        path: '',
        component: JobDemandsComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class JobDemandRoutingModule {}
