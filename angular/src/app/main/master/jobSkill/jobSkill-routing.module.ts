import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JobSkillComponent } from './jobSkill.component';

const routes: Routes = [
    {
        path: '',
        component: JobSkillComponent,
        pathMatch: 'full',
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class JobSkillRoutingModule {}
