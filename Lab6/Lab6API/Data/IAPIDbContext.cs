using Lab6API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Data
{
    public interface IAPIDbContext : IDisposable
    {
        public DbSet<StaffCategories> StaffCategories { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<StaffPatientAssociations> StaffPatientAssociations { get; set; }
        public DbSet<RecordComponents> RecordComponents { get; set; }
        public DbSet<PatientRecords> PatientRecords { get; set; }
        public DbSet<AppointmentStatusCodes> AppointmentStatusCodes { get; set; }
        public DbSet<Appointments> Appointments { get; set; }

        public Task<bool> CanConnectAsync();
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
