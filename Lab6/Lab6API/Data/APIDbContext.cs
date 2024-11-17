using Lab6API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Data
{
    public class APIDbContext : DbContext, IAPIDbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options)
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
    }
}
