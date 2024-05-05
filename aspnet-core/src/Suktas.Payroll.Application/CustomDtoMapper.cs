using Suktas.Payroll.Job.Dtos;
using Suktas.Payroll.Job;
using Suktas.Payroll.Master.Dtos;
using Suktas.Payroll.Master;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Extensions;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using Suktas.Payroll.Auditing.Dto;
using Suktas.Payroll.Authorization.Accounts.Dto;
using Suktas.Payroll.Authorization.Delegation;
using Suktas.Payroll.Authorization.Permissions.Dto;
using Suktas.Payroll.Authorization.Roles;
using Suktas.Payroll.Authorization.Roles.Dto;
using Suktas.Payroll.Authorization.Users;
using Suktas.Payroll.Authorization.Users.Delegation.Dto;
using Suktas.Payroll.Authorization.Users.Dto;
using Suktas.Payroll.Authorization.Users.Profile.Dto;
using Suktas.Payroll.Chat;
using Suktas.Payroll.Chat.Dto;
using Suktas.Payroll.DynamicEntityProperties.Dto;
using Suktas.Payroll.Editions;
using Suktas.Payroll.Editions.Dto;
using Suktas.Payroll.Friendships;
using Suktas.Payroll.Friendships.Cache;
using Suktas.Payroll.Friendships.Dto;
using Suktas.Payroll.Localization.Dto;
using Suktas.Payroll.MultiTenancy;
using Suktas.Payroll.MultiTenancy.Dto;
using Suktas.Payroll.MultiTenancy.HostDashboard.Dto;
using Suktas.Payroll.MultiTenancy.Payments;
using Suktas.Payroll.MultiTenancy.Payments.Dto;
using Suktas.Payroll.Notifications.Dto;
using Suktas.Payroll.Organizations.Dto;
using Suktas.Payroll.Sessions.Dto;
using Suktas.Payroll.WebHooks.Dto;

namespace Suktas.Payroll
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrEditEmploymentDto, Employment>().ReverseMap();
            configuration.CreateMap<EmploymentDto, Employment>().ReverseMap();
            configuration.CreateMap<CreateOrEditJobApplyDto, JobApply>().ReverseMap();
            configuration.CreateMap<JobApplyDto, JobApply>().ReverseMap();
            configuration.CreateMap<CreateOrEditJobDemandDto, JobDemand>().ReverseMap();
            configuration.CreateMap<JobDemandDto, JobDemand>().ReverseMap();
            configuration.CreateMap<CreateOrEditEmployeeDto, Employee>().ReverseMap();
            configuration.CreateMap<EmployeeDto, Employee>().ReverseMap();
            configuration.CreateMap<CreateOrEditCompanyDto, Company>().ReverseMap();
            configuration.CreateMap<CompanyDto, Company>().ReverseMap();
            configuration.CreateMap<CreateOrEditQualificationDto, Qualification>().ReverseMap();
            configuration.CreateMap<QualificationDto, Qualification>().ReverseMap();
            configuration.CreateMap<CreateOrEditCompanyCategoryDto, CompanyCategory>().ReverseMap();
            configuration.CreateMap<CompanyCategoryDto, CompanyCategory>().ReverseMap();
            configuration.CreateMap<CreateOrEditCompanyTypeDto, CompanyType>().ReverseMap();
            configuration.CreateMap<CompanyTypeDto, CompanyType>().ReverseMap();
            configuration.CreateMap<CreateOrEditJobSkillDto, JobSkill>().ReverseMap();
            configuration.CreateMap<JobSkillDto, JobSkill>().ReverseMap();

            configuration.CreateMap<CreateOrEditFinancialYearDto, FinancialYear>().ReverseMap();
            configuration.CreateMap<FinancialYearDto, FinancialYear>().ReverseMap();

            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(dto => dto.DynamicPropertyName,
                    options => options.MapFrom(entity => entity.DynamicProperty.DisplayName.IsNullOrEmpty() ? entity.DynamicProperty.PropertyName : entity.DynamicProperty.DisplayName));
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();

            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */
        }
    }
}