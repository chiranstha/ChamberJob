using Suktas.Payroll.Job;
using Suktas.Payroll.Master;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Suktas.Payroll.Authorization.Delegation;
using Suktas.Payroll.Authorization.Roles;
using Suktas.Payroll.Authorization.Users;
using Suktas.Payroll.Chat;
using Suktas.Payroll.Editions;
using Suktas.Payroll.Friendships;
using Suktas.Payroll.MultiTenancy;
using Suktas.Payroll.MultiTenancy.Accounting;
using Suktas.Payroll.MultiTenancy.Payments;
using Suktas.Payroll.Storage;

namespace Suktas.Payroll.EntityFrameworkCore
{
    public class PayrollDbContext : AbpZeroDbContext<Tenant, Role, User, PayrollDbContext>
    {
        public virtual DbSet<Employment> Employments { get; set; }

        public virtual DbSet<JobApply> JobApply { get; set; }

        public virtual DbSet<JobDemand> JobDemands { get; set; }

        public virtual DbSet<Employee> Employee { get; set; }

        public virtual DbSet<Company> Company { get; set; }

        public virtual DbSet<Qualification> Qualification { get; set; }

        public virtual DbSet<CompanyCategory> CompanyCategory { get; set; }

        public virtual DbSet<CompanyType> CompanyType { get; set; }

        public virtual DbSet<JobSkill> JobSkill { get; set; }

        public virtual DbSet<FinancialYear> FinancialYears { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<RecentPassword> RecentPasswords { get; set; }

        public PayrollDbContext(DbContextOptions<PayrollDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employment>(x =>
            {
                x.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<JobApply>(j =>
                       {
                           j.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<JobDemand>(j =>
                       {
                           j.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Employee>(x =>
                       {
                           x.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Company>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Qualification>(q =>
                       {
                           q.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<CompanyCategory>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<CompanyType>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<JobSkill>(j =>
                       {
                           j.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<FinancialYear>(f =>
                       {
                           f.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<BinaryObject>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique()
                    .HasFilter("[IsDeleted] = 0");
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });
        }
    }
}