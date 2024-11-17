using Lab6API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Data
{
    public class PostgreSqlDbContext: DbContext, IAPIDbContext
    {
        public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options)
            : base(options)
        {
        }
        public DbSet<StaffCategories> StaffCategories { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<StaffPatientAssociations> StaffPatientAssociations { get; set; }
        public DbSet<RecordComponents> RecordComponents { get; set; }
        public DbSet<PatientRecords> PatientRecords { get; set; }
        public DbSet<AppointmentStatusCodes> AppointmentStatusCodes { get; set; }
        public DbSet<Appointments> Appointments { get; set; }

        public async Task<bool> CanConnectAsync()
        {
            return await this.Database.CanConnectAsync();
        }

        public async Task<int> SaveShangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add correct data type for postgresql
            // Guid -> uuid
            // string -> text

            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnType("uuid");
                entity.Property(e => e.AppointmentDetails).HasColumnType("text");
            });
            modelBuilder.Entity<AppointmentStatusCodes>(entity =>
            {
                entity.Property(e => e.AppointmentStatusCode).HasColumnType("uuid");
                entity.Property(e => e.AppointmentStatusDescription).HasColumnType("text");
            });
            modelBuilder.Entity<PatientRecords>(entity =>
            {
                entity.Property(e => e.PatientRecordId).HasColumnType("uuid");
                entity.Property(e => e.PatientId).HasColumnType("uuid");
                entity.Property(e => e.ComponentCode).HasColumnType("uuid");
                entity.Property(e => e.PatientRecordComponentDetails).HasColumnType("text");
                entity.Property(e => e.UpdatedByStaffId).HasColumnType("uuid");
            });
            modelBuilder.Entity<Patients>(entity =>
            {
                entity.Property(e => e.PattientId).HasColumnType("uuid");
                entity.Property(e => e.CommanagedYn).HasColumnType("text");
                entity.Property(e => e.NhsNumber).HasColumnType("text");
                entity.Property(e => e.Gender).HasColumnType("text");
                entity.Property(e => e.PatientAddress).HasColumnType("text");
                entity.Property(e => e.Details).HasColumnType("text");
            });
            modelBuilder.Entity<RecordComponents>(entity =>
            {
                entity.Property(e => e.ComponentCode).HasColumnType("uuid");
                entity.Property(e => e.ComponentDescription).HasColumnType("text");
            });
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.RoleCode).HasColumnType("uuid");
                entity.Property(e => e.RoleDescription).HasColumnType("text");
            });
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffId).HasColumnType("uuid");
                entity.Property(e => e.Gender).HasColumnType("text");
                entity.Property(e => e.StaffFirstName).HasColumnType("text");
                entity.Property(e => e.StaffLastName).HasColumnType("text");
                entity.Property(e => e.StaffMiddleName).HasColumnType("text");
                entity.Property(e => e.StaffDetails).HasColumnType("text");
                entity.Property(e => e.StaffQualifications).HasColumnType("text");
                entity.Property(e => e.StaffCategoryCode).HasColumnType("uuid");
                entity.Property(e => e.RoleCode).HasColumnType("uuid");
            });
            modelBuilder.Entity<StaffCategories>(entity =>
            {
                entity.Property(e => e.StaffCategoryCode).HasColumnType("uuid");
                entity.Property(e => e.StaffCategoryDescription).HasColumnType("text");
            });
            modelBuilder.Entity<StaffPatientAssociations>(entity =>
            {
                entity.Property(e => e.AssociationId).HasColumnType("uuid");
                entity.Property(e => e.AssociationDetails).HasColumnType("text");
                entity.Property(e => e.PatientId).HasColumnType("uuid");
                entity.Property(e => e.StaffId).HasColumnType("uuid");
            });
        }
    }
}
