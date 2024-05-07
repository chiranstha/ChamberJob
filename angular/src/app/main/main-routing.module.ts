import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    
                    {
                        path: 'company/employments',
                        loadChildren: () => import('./company/employments/employment.module').then(m => m.EmploymentModule),
                        data: { permission: 'Pages.Employments' }
                    },
                
                    
                    {
                        path: 'job/jobApply',
                        loadChildren: () => import('./job/jobApply/jobApply.module').then(m => m.JobApplyModule),
                        data: { permission: 'Pages.JobApply' }
                    },
                
                    
                    {
                        path: 'job/jobDemands',
                        loadChildren: () => import('./job/jobDemands/jobDemand.module').then(m => m.JobDemandModule),
                        data: { permission: 'Pages.JobDemands' }
                    },
                
                    
                  
                    
                    {
                        path: 'master/company',
                        loadChildren: () => import('./master/company/company.module').then(m => m.CompanyModule),
                        data: { permission: 'Pages.Company' }
                    },
                
                    
                    {
                        path: 'master/qualification',
                        loadChildren: () => import('./master/qualification/qualification.module').then(m => m.QualificationModule),
                        data: { permission: 'Pages.Qualification' }
                    },

                    {
                        path: 'master/qualification',
                        loadChildren: () => import('./master/qualification/qualification.module').then(m => m.QualificationModule),
                        data: { permission: 'Pages.Qualification' }
                    },

                    
                
                    
                  

                   
                
                    
                    {
                        path: 'master/companyType',
                        loadChildren: () => import('./master/companyType/companyType.module').then(m => m.CompanyTypeModule),
                        data: { permission: 'Pages.CompanyType' }
                    },
                
                    
                    {
                        path: 'master/jobSkill',
                        loadChildren: () => import('./master/jobSkill/jobSkill.module').then(m => m.JobSkillModule),
                        data: { permission: 'Pages.JobSkill' }
                    },


                    
                    {
                        path: 'master/companyCategory',
                        loadChildren: () => import('./master/companyCategory/companyCategory.module').then(m => m.CompanyCategoryModule),
                        data: { permission: 'Pages.JobSkill' }
                    },

                     
                    {
                        path: 'jobAgency',
                        loadChildren: () => import('./jobAgency/jobAgency.module').then(m => m.JobAgencyModule),
                        data: { permission: 'Pages.JobSkill' }
                    },
                
                    
                   
                    
                    {
                        path: 'master/financialYears',
                        loadChildren: () => import('./master/financialYears/financialYear.module').then(m => m.FinancialYearModule),
                        data: { permission: 'Pages.FinancialYears' }
                    },
                
                    
                   
                
                    
                  
                
                    {
                        path: 'dashboard',
                        loadChildren: () => import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
                        data: {  },
                    },
                    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
                    { path: '**', redirectTo: 'dashboard' },
                ],
            },
        ]),
    ],
    exports: [RouterModule],
})
export class MainRoutingModule {}
