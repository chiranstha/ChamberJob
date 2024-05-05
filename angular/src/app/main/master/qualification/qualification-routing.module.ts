import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { QualificationComponent } from './qualification.component';

const routes: Routes = [
    {
        path: '',
        component: QualificationComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class QualificationRoutingModule {}
