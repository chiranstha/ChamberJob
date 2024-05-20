import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/common/session/app-session.service';
import { UserTypeEnum } from '@shared/service-proxies/service-proxies';

import { Injectable } from '@angular/core';
import { AppMenu } from './app-menu';
import { AppMenuItem } from './app-menu-item';

@Injectable()
export class AppNavigationService {
    constructor(
        private _permissionCheckerService: PermissionCheckerService,
        private _appSessionService: AppSessionService
    ) { }

    getMenu(): AppMenu {

        if (this._appSessionService.user.userType === UserTypeEnum.System) {
            return new AppMenu('MainMenu', 'MainMenu', [
                new AppMenuItem(
                    'Dashboard',
                    'Pages.Administration.Host.Dashboard',
                    'fa-regular fa-computer',
                    '/app/admin/hostDashboard'
                ),
                new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'fa-regular fa-computer', '/app/main/dashboard'),
                new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
                new AppMenuItem(
                    'Master',
                    '',
                    'fa-regular fa-house',
                    '',
                    [],
                    [

                        new AppMenuItem('JobSkill', 'Pages.JobSkill', 'flaticon-more', '/app/main/master/jobSkill'),

                        new AppMenuItem('CompanyType', 'Pages.CompanyType', 'flaticon-more', '/app/main/master/companyType'),

                        new AppMenuItem('CompanyCategory', 'Pages.CompanyCategory', 'flaticon-more', '/app/main/master/companyCategory'),

                        new AppMenuItem('Qualification', 'Pages.Qualification', 'flaticon-more', '/app/main/master/qualification'),
                        new AppMenuItem('FinancialYears', 'Pages.FinancialYears', 'flaticon-more', '/app/main/master/financialYears'),
                    ]),

                new AppMenuItem(
                    'Job Application',
                    '',
                    'fa-regular fa-house',
                    '',
                    [],
                    [

                        new AppMenuItem('Company', '', 'fa-regular fa-briefcase', '/app/main/jobAgency/company'),
                        new AppMenuItem('Employee', '', 'fa-regular fa-briefcase', '/app/main/jobAgency/employee'),
                        new AppMenuItem('JobDemand', '', 'fa-regular fa-briefcase', '/app/main/jobAgency/jobdemand'),
                        new AppMenuItem('Employment', '', 'fa-regular fa-briefcase', '/app/main/jobAgency/employments'),






                        // new AppMenuItem('JobDemands', 'Pages.JobDemands', 'flaticon-more', '/app/main/job/jobDemands'),

                        // new AppMenuItem('JobApply', 'Pages.JobApply', 'flaticon-more', '/app/main/job/jobApply'),

                        // new AppMenuItem('Employments', 'Pages.Employments', 'flaticon-more', '/app/main/company/employments'),
                    ]),






                new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),
                new AppMenuItem(
                    'Administration',
                    '',
                    'flaticon-interface-8',
                    '',
                    [],
                    [
                        new AppMenuItem(
                            'OrganizationUnits',
                            'Pages.Administration.OrganizationUnits',
                            'flaticon-map',
                            '/app/admin/organization-units'
                        ),
                        new AppMenuItem('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
                        new AppMenuItem('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
                        new AppMenuItem(
                            'Languages',
                            'Pages.Administration.Languages',
                            'fa-sharp fa-language',
                            '/app/admin/languages',
                            ['/app/admin/languages/{name}/texts']
                        ),
                        new AppMenuItem(
                            'AuditLogs',
                            'Pages.Administration.AuditLogs',
                            'flaticon-folder-1',
                            '/app/admin/auditLogs'
                        ),
                        new AppMenuItem(
                            'Maintenance',
                            'Pages.Administration.Host.Maintenance',
                            'flaticon-lock',
                            '/app/admin/maintenance'
                        ),
                        new AppMenuItem(
                            'Subscription',
                            'Pages.Administration.Tenant.SubscriptionManagement',
                            'flaticon-refresh',
                            '/app/admin/subscription-management'
                        ),
                        new AppMenuItem(
                            'VisualSettings',
                            'Pages.Administration.UiCustomization',
                            'flaticon-medical',
                            '/app/admin/ui-customization'
                        ),
                        new AppMenuItem(
                            'WebhookSubscriptions',
                            'Pages.Administration.WebhookSubscription',
                            'flaticon2-world',
                            '/app/admin/webhook-subscriptions'
                        ),
                        new AppMenuItem(
                            'DynamicProperties',
                            'Pages.Administration.DynamicProperties',
                            'flaticon-interface-8',
                            '/app/admin/dynamic-property'
                        ),
                        new AppMenuItem(
                            'Settings',
                            'Pages.Administration.Host.Settings',
                            'flaticon-settings',
                            '/app/admin/hostSettings'
                        ),
                        new AppMenuItem(
                            'Settings',
                            'Pages.Administration.Tenant.Settings',
                            'flaticon-settings',
                            '/app/admin/tenantSettings'
                        ),
                        new AppMenuItem(
                            'Notifications',
                            '',
                            'fa-regular fa-bell',
                            '',
                            [],
                            [
                                new AppMenuItem(
                                    'Inbox',
                                    '',
                                    'flaticon-mail-1',
                                    '/app/notifications'
                                ),
                                new AppMenuItem(
                                    'MassNotifications',
                                    'Pages.Administration.MassNotification',
                                    'flaticon-paper-plane',
                                    '/app/admin/mass-notifications'
                                )
                            ]
                        )
                    ]
                ),
                // new AppMenuItem(
                //     'DemoUiComponents',
                //     'Pages.DemoUiComponents',
                //     'flaticon-shapes',
                //     '/app/admin/demo-ui-components'
                // ),
            ]);
        } else if (this._appSessionService.user.userType === UserTypeEnum.Company) {
            return new AppMenu('MainMenu', 'MainMenu', [

                new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'fa-regular fa-computer', '/app/main/dashboard'),

                new AppMenuItem('Company', 'Pages.Company', 'fa-duotone fa-buildings', '/app/main/master/company'),
                new AppMenuItem('Employments', 'Pages.Employments', 'fa-duotone fa-person-dress', '/app/main/company/employments'),

                new AppMenuItem('JobDemands', 'Pages.JobDemands', 'fa-duotone fa-briefcase', '/app/main/job/jobDemands'),

                new AppMenuItem('JobApply', 'Pages.JobApply', 'fa-duotone fa-person-running', '/app/main/job/jobApply'),










            ]);
        } else {

            return new AppMenu('MainMenu', 'MainMenu', [

                new AppMenuItem('Dashboard', '', 'fa-regular fa-computer', '/app/main/dashboard'),



                new AppMenuItem('Employee', '', 'flaticon-more', '/app/employment/employee'),

                new AppMenuItem('JobDemands', '', 'flaticon-more', '/app/employment/jobdemand'),

                new AppMenuItem('JobApply', '', 'flaticon-more', '/app/employment/jobapply'),

               // new AppMenuItem('Employments', 'Pages.Employments', 'flaticon-more', '/app/main/company/employments'),









            ]);

        }
    }

    checkChildMenuItemPermission(menuItem): boolean {
        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];

            if (subMenuItem.permissionName === '' || subMenuItem.permissionName === null) {
                if (subMenuItem.route) {
                    return true;
                }
            } else if (this._permissionCheckerService.isGranted(subMenuItem.permissionName)) {
                if (!subMenuItem.hasFeatureDependency()) {
                    return true;
                }

                if (subMenuItem.featureDependencySatisfied()) {
                    return true;
                }
            }

            if (subMenuItem.items && subMenuItem.items.length) {
                let isAnyChildItemActive = this.checkChildMenuItemPermission(subMenuItem);
                if (isAnyChildItemActive) {
                    return true;
                }
            }
        }

        return false;
    }

    showMenuItem(menuItem: AppMenuItem): boolean {
        if (
            menuItem.permissionName === 'Pages.Administration.Tenant.SubscriptionManagement' &&
            this._appSessionService.tenant &&
            !this._appSessionService.tenant.edition
        ) {
            return false;
        }

        let hideMenuItem = false;

        if (menuItem.requiresAuthentication && !this._appSessionService.user) {
            hideMenuItem = true;
        }

        if (menuItem.permissionName && !this._permissionCheckerService.isGranted(menuItem.permissionName)) {
            hideMenuItem = true;
        }

        if (this._appSessionService.tenant || !abp.multiTenancy.ignoreFeatureCheckForHostUsers) {
            if (menuItem.hasFeatureDependency() && !menuItem.featureDependencySatisfied()) {
                hideMenuItem = true;
            }
        }

        if (!hideMenuItem && menuItem.items && menuItem.items.length) {
            return this.checkChildMenuItemPermission(menuItem);
        }

        return !hideMenuItem;
    }

    /**
     * Returns all menu items recursively
     */
    getAllMenuItems(): AppMenuItem[] {
        let menu = this.getMenu();
        let allMenuItems: AppMenuItem[] = [];
        menu.items.forEach((menuItem) => {
            allMenuItems = allMenuItems.concat(this.getAllMenuItemsRecursive(menuItem));
        });

        return allMenuItems;
    }

    private getAllMenuItemsRecursive(menuItem: AppMenuItem): AppMenuItem[] {
        if (!menuItem.items) {
            return [menuItem];
        }

        let menuItems = [menuItem];
        menuItem.items.forEach((subMenu) => {
            menuItems = menuItems.concat(this.getAllMenuItemsRecursive(subMenu));
        });

        return menuItems;
    }
}
