using Lab6API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Data
{
    public class SqliteDbContext : DbContext, IAPIDbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options)
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

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // add corect data types for sqlite
            // Guid -> TEXT
            // string -> TEXT
            modelBuilder.Entity<Appointments>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnType("TEXT");
                entity.Property(e => e.AppointmentDetails).HasColumnType("TEXT");
            });
            modelBuilder.Entity<AppointmentStatusCodes>(entity =>
            {
                entity.Property(e => e.AppointmentStatusCode).HasColumnType("TEXT");
                entity.Property(e => e.AppointmentStatusDescription).HasColumnType("TEXT");
            });
            modelBuilder.Entity<PatientRecords>(entity =>
            {
                entity.Property(e => e.PatientRecordId).HasColumnType("TEXT");
                entity.Property(e => e.PatientId).HasColumnType("TEXT");
                entity.Property(e => e.ComponentCode).HasColumnType("TEXT");
                entity.Property(e => e.PatientRecordComponentDetails).HasColumnType("TEXT");
                entity.Property(e => e.UpdatedByStaffId).HasColumnType("TEXT");
            });
            modelBuilder.Entity<Patients>(entity =>
            {
                entity.Property(e => e.PattientId).HasColumnType("TEXT");
                entity.Property(e => e.CommanagedYn).HasColumnType("TEXT");
                entity.Property(e => e.NhsNumber).HasColumnType("TEXT");
                entity.Property(e => e.Gender).HasColumnType("TEXT");
                entity.Property(e => e.PatientAddress).HasColumnType("TEXT");
                entity.Property(e => e.Details).HasColumnType("TEXT");
            });
            modelBuilder.Entity<RecordComponents>(entity =>
            {
                entity.Property(e => e.ComponentCode).HasColumnType("TEXT");
                entity.Property(e => e.ComponentDescription).HasColumnType("TEXT");
            });
            modelBuilder.Entity<Roles>(entity =>
            {
                entity.Property(e => e.RoleCode).HasColumnType("TEXT");
                entity.Property(e => e.RoleDescription).HasColumnType("TEXT");
            });
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(e => e.StaffId).HasColumnType("TEXT");
                entity.Property(e => e.Gender).HasColumnType("TEXT");
                entity.Property(e => e.StaffFirstName).HasColumnType("TEXT");
                entity.Property(e => e.StaffLastName).HasColumnType("TEXT");
                entity.Property(e => e.StaffMiddleName).HasColumnType("TEXT");
                entity.Property(e => e.StaffDetails).HasColumnType("TEXT");
                entity.Property(e => e.StaffQualifications).HasColumnType("TEXT");
                entity.Property(e => e.StaffCategoryCode).HasColumnType("TEXT");
                entity.Property(e => e.RoleCode).HasColumnType("TEXT");
            });
            modelBuilder.Entity<StaffCategories>(entity =>
            {
                entity.Property(e => e.StaffCategoryCode).HasColumnType("TEXT");
                entity.Property(e => e.StaffCategoryDescription).HasColumnType("TEXT");
            });
            modelBuilder.Entity<StaffPatientAssociations>(entity =>
            {
                entity.Property(e => e.AssociationId).HasColumnType("TEXT");
                entity.Property(e => e.AssociationDetails).HasColumnType("TEXT");
                entity.Property(e => e.PatientId).HasColumnType("TEXT");
                entity.Property(e => e.StaffId).HasColumnType("TEXT");
            });
        }
    }
}
