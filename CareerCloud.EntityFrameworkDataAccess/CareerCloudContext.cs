using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {
        public CareerCloudContext(DbContextOptions<CareerCloudContext> options): base (options)
        {

        }
        public DbSet<ApplicantEducationPoco> ApplicationEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJob { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfile { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connStr = root.GetSection("ConnectionStrings").
                GetSection("CareerCloudContext").Value;
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(_connStr);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>
                (e =>
                {
                    e.HasKey(e => e.Id);
                    e.HasOne(i => i.ApplicantProfilePoco)
                    .WithMany(p => p.ApplicantEducations)
                    .HasForeignKey(e => e.Applicant);
                    e.Property(e => e.TimeStamp).IsRowVersion();
                });

            modelBuilder.Entity<SecurityLoginPoco>
                (e =>
                {
                    e.HasKey(e => e.Id);
                    e.HasMany(i => i.ApplicantProfiles)
                    .WithOne(p => p.SecurityLogin)
                    .HasForeignKey(e => e.Login);
                    e.Property(e => e.TimeStamp).IsRowVersion();
                });
            modelBuilder.Entity<ApplicantResumePoco>
               (e =>
               {
                   e.HasKey(e => e.Id);
                   e.HasOne(i => i.ApplicantProfile)
                   .WithMany(p => p.ApplicantResumes)
                   .HasForeignKey(e => e.Applicant);
               });

            modelBuilder.Entity<ApplicantSkillPoco>
               (e =>
               {
                   e.ToTable("Applicant_Skills");
                   e.HasKey(e => e.Id);
                   e.HasOne(i => i.ApplicantProfile)
                   .WithMany(p => p.ApplicantSkills)
                   .HasForeignKey(e => e.Applicant);
               });

            modelBuilder.Entity<SecurityLoginsLogPoco>
               (e =>
               {
                   e.ToTable("Security_Logins_Log");
                   e.HasKey(e => e.Id);
                   e.HasOne(i => i.SecurityLogin)
                   .WithMany(p => p.SecurityLoginsLogs)
                   .HasForeignKey(e => e.Login);
               });

            modelBuilder.Entity<SecurityLoginsRolePoco>
               (e =>
               {
                   e.HasKey(e => e.Id);
                   e.HasOne(i => i.SecurityLogin)
                   .WithMany(p => p.SecurityLoginsRoles)
                   .HasForeignKey(e => e.Login);
               });

            modelBuilder.Entity<SecurityLoginsRolePoco>
               (e =>
               {
                   e.HasKey(e => e.Id);
                   e.HasOne(i => i.SecurityRole)
                   .WithMany(p => p.SecurityLoginsRoles)
                   .HasForeignKey(e => e.Role);
               });
            modelBuilder.Entity<ApplicantWorkHistoryPoco>
               (e =>
               {
                   e.HasKey(e => e.Id);
                   e.HasOne(i => i.ApplicantProfile)
                   .WithMany(p => p.ApplicantWorkHistory)
                   .HasForeignKey(e => e.Applicant);
               });

            modelBuilder.Entity<ApplicantWorkHistoryPoco>
               (e =>
               {
                   e.HasKey(e => e.Id);
                   e.HasOne(i => i.SystemCountryCode)
                   .WithMany(p => p.ApplicantWorkHistorys)
                   .HasForeignKey(e => e.CountryCode);
               });

            modelBuilder.Entity<ApplicantJobApplicationPoco>
              (e =>
              {
                  e.HasKey(e => e.Id);
                  e.HasOne(i => i.ApplicantProfilePoco)
                  .WithMany(p => p.ApplicantJobApplications)
                  .HasForeignKey(e => e.Applicant);
              });

            modelBuilder.Entity<ApplicantProfilePoco>
             (e =>
             {
                 e.HasOne(i => i.SystemCountryCode)
                 .WithMany(p => p.ApplicantProfiles)
                 .HasForeignKey(e => e.Country);
             });

            modelBuilder.Entity<ApplicantJobApplicationPoco>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(o => o.CompanyJob)
                .WithMany(m => m.ApplicantJobApplications)
                .HasForeignKey(f => f.Job);
            });

            modelBuilder.Entity<CompanyProfilePoco>(e =>
            {
                e.HasKey(k => k.Id);
                e.Property(e => e.TimeStamp).IsRowVersion();

            });
            modelBuilder.Entity<CompanyDescriptionPoco>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(o => o.CompanyProfile)
                .WithMany(m => m.CompanyDescriptions)
                .HasForeignKey(f => f.Company);
            });

            modelBuilder.Entity<CompanyJobPoco>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(o => o.CompanyProfile)
                .WithMany(m => m.CompanyJobs)
                .HasForeignKey(k => k.Company);
            });
            modelBuilder.Entity<CompanyJobDescriptionPoco>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(o => o.CompanyJob)
                .WithMany(m => m.CompanyJobDescriptions)
                .HasForeignKey(f => f.Job);
            });

            modelBuilder.Entity<CompanyLocationPoco>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(o => o.CompanyProfile)
                .WithMany(m => m.CompanyLocations)
                .HasForeignKey(k => k.Company);
            });

            modelBuilder.Entity<CompanyJobEducationPoco>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(o => o.CompanyJob)
                .WithMany(m => m.CompanyJobEducations)
                .HasForeignKey(f => f.Job);
            });
            modelBuilder.Entity<CompanyJobSkillPoco>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(c => c.CompanyJob)
                .WithMany(m => m.CompanyJobSkills)
                .HasForeignKey(f => f.Job);
            });

            modelBuilder.Entity<CompanyDescriptionPoco>(e =>
            {
                e.HasKey(k => k.Id);
                e.HasOne(c => c.SystemLanguageCode)
                .WithMany(m => m.CompanyDescriptions)
                .HasForeignKey(f => f.LanguageId);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
