using Lab6API.Data;
using Lab6API.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6API.Service
{
    public static class DbInitialize
    {
        private static DateTimeOffset GetDate(int year, int month, int day)
        {
            var date = new DateTime(year, month, day);
            var specify = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            var targetTime = new DateTimeOffset(specify);
            return targetTime;
        }
        private static DateTimeOffset GetDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            var date = new DateTime(year, month, day, hour, minute, second);
            var specify = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            var targetTime = new DateTimeOffset(specify);
            return targetTime;
        }
        public static async Task InitializeStaffCategories(IAPIDbContext context)
        {
            if(!await context.CanConnectAsync() || context.StaffCategories.Any())
                return;

            await context.StaffCategories.AddRangeAsync(
                new StaffCategories
                {
                    StaffCategoryCode = Guid.NewGuid(),
                    StaffCategoryDescription = "Doctor"
                },
                new StaffCategories
                {
                    StaffCategoryCode = Guid.NewGuid(),
                    StaffCategoryDescription = "Midwife"
                }
            );
            await context.SaveChangesAsync();
        }

        public static async Task InitializeRoles(IAPIDbContext context)
        {
            if(!await context.CanConnectAsync() || context.Roles.Any())
                return;
            
            await context.Roles.AddRangeAsync(
                new Roles
                {
                    RoleCode = Guid.NewGuid(),
                    RoleDescription = "Administrators"
                },
                new Roles
                {
                    RoleCode = Guid.NewGuid(),
                    RoleDescription = "Nurses"
                },
                new Roles
                {
                    RoleCode = Guid.NewGuid(),
                    RoleDescription = "Physicians"
                },
                new Roles
                {
                    RoleCode = Guid.NewGuid(),
                    RoleDescription = "Surgeons"
                },
                new Roles
                {
                    RoleCode = Guid.NewGuid(),
                    RoleDescription = "Interpreters"
                },
                new Roles
                {
                    RoleCode = Guid.NewGuid(),
                    RoleDescription = "Radiographers"
                }
            );
            await context.SaveChangesAsync();
        }

        public static async Task InitializeStaff(IAPIDbContext context)
        {
            if (!await context.CanConnectAsync() || context.Staffs.Any())
                return;

            var roles = await context.Roles.ToListAsync();
            var staff_categories = await context.StaffCategories.ToListAsync();
            await context.Staffs.AddRangeAsync(
                new Staff
                {
                    StaffId = Guid.NewGuid(),
                    StaffCategory = staff_categories[0],
                    Role = roles[0],
                    Gender = "male",
                    StaffFirstName = "Lucas",
                    StaffLastName = "Hall",
                    StaffMiddleName = "Arellano",
                    StaffQualifications = roles[0].RoleDescription,
                    StaffBirthDate = DbInitialize.GetDate(1977, 12, 20),
                    StaffDetails = "These people are administrators"
                },
                new Staff
                {
                    StaffId = Guid.NewGuid(),
                    StaffCategory = staff_categories[1],
                    Role = roles[1],
                    Gender = "female",
                    StaffFirstName = "Ariana",
                    StaffLastName = "Cheyanne",
                    StaffMiddleName = "Hodges",
                    StaffQualifications = roles[1].RoleDescription,
                    StaffBirthDate = DbInitialize.GetDate(1990, 9, 12),
                    StaffDetails = "These people are nurses"
                },
                new Staff
                {
                    StaffId = Guid.NewGuid(),
                    StaffCategory = staff_categories[0],
                    Role = roles[2],
                    Gender = "male",
                    StaffFirstName = "Laney",
                    StaffLastName = "Sheridan",
                    StaffMiddleName = "Spalding",
                    StaffQualifications = roles[2].RoleDescription,
                    StaffBirthDate = DbInitialize.GetDate(1989, 6, 30),
                    StaffDetails = "These people are physicians"
                },
                new Staff
                {
                    StaffId = Guid.NewGuid(),
                    StaffCategory = staff_categories[1],
                    Role = roles[3],
                    Gender = "female",
                    StaffFirstName = "Gabriella",
                    StaffLastName = "Emery",
                    StaffMiddleName = "Morrison",
                    StaffQualifications = roles[3].RoleDescription,
                    StaffBirthDate = DbInitialize.GetDate(1995, 2, 1),
                    StaffDetails = "These people are surgeons"
                },
                new Staff
                {
                    StaffId = Guid.NewGuid(),
                    StaffCategory = staff_categories[0],
                    Role = roles[4],
                    Gender = "male",
                    StaffFirstName = "Nikita",
                    StaffLastName = "Jaqueline",
                    StaffMiddleName = "Masterson",
                    StaffQualifications = roles[4].RoleDescription,
                    StaffBirthDate = DbInitialize.GetDate(1993, 8, 24),
                    StaffDetails = "These people are interpreters"
                },
                new Staff
                {
                    StaffId = Guid.NewGuid(),
                    StaffCategory = staff_categories[1],
                    Role = roles[5],
                    Gender = "female",
                    StaffFirstName = "Ana",
                    StaffLastName = "Bobbi",
                    StaffMiddleName = "Arellano",
                    StaffQualifications = roles[5].RoleDescription,
                    StaffBirthDate = DbInitialize.GetDate(1997, 9, 1),
                    StaffDetails = "These people are radiographers"
                }
            );
            await context.SaveChangesAsync();
        }

        public static async Task InitializePatients(IAPIDbContext context)
        {
            if (!await context.CanConnectAsync() || context.Patients.Any())
                return;

            await context.Patients.AddRangeAsync(
                new Patients
                {
                    PattientId = Guid.NewGuid(),
                    CommanagedYn = "Yes",
                    NhsNumber = "1234567890",
                    Gender = "female",
                    DateOfBirth = DbInitialize.GetDate(1983, 11, 10),
                    PatientName = "Chloe Lewis",
                    PatientAddress = "3 Internatsionala Ul., bld. 106",
                    Height = 175,
                    Weight = 55,
                    Details = "blood type I"
                },
                new Patients
                {
                    PattientId = Guid.NewGuid(),
                    CommanagedYn = "Yes",
                    NhsNumber = "0987654321",
                    Gender = "male",
                    DateOfBirth = DbInitialize.GetDate(1990, 10, 18),
                    PatientName = "Grase Remirez",
                    PatientAddress = "Nizhniy Val Ul., bld. 19",
                    Height = 182,
                    Weight = 70,
                    Details = "blood type II"
                },
                new Patients
                {
                    PattientId = Guid.NewGuid(),
                    CommanagedYn = "Yes",
                    NhsNumber = "0897654321",
                    Gender = "female",
                    DateOfBirth = DbInitialize.GetDate(2001, 1, 21),
                    PatientName = "Violet Garcia",
                    PatientAddress = "Sumi / 6-A Prodolna, Vul., bld. 21",
                    Height = 160,
                    Weight = 48,
                    Details = "blood type I"
                },
                new Patients
                {
                    PattientId = Guid.NewGuid(),
                    CommanagedYn = "Yes",
                    NhsNumber = "1324756890",
                    Gender = "male",
                    DateOfBirth = DbInitialize.GetDate(1999, 5, 14),
                    PatientName = "Iris Senders",
                    PatientAddress = "Bulvarnyy Per., bld. 6, appt. 44",
                    Height = 173,
                    Weight = 65,
                    Details = "blood type III"
                },
                new Patients
                {
                    PattientId = Guid.NewGuid(),
                    CommanagedYn = "Yes",
                    NhsNumber = "1234567890",
                    Gender = "female",
                    DateOfBirth = DbInitialize.GetDate(1995, 4, 1),
                    PatientName = "Naomi Gomez",
                    PatientAddress = "10 Let Oktyabrya Ul., bld. 13",
                    Height = 183,
                    Weight = 51,
                    Details = "blood type IV"
                },
                new Patients
                {
                    PattientId = Guid.NewGuid(),
                    CommanagedYn = "Yes",
                    NhsNumber = "1234567890",
                    Gender = "male",
                    DateOfBirth = DbInitialize.GetDate(1981, 7, 22),
                    PatientName = "Ryan Parker",
                    PatientAddress = "Karbysheva Ul., bld. 28, appt. 52",
                    Height = 190,
                    Weight = 80,
                    Details = "blood type I"
                }
            );
            await context.SaveChangesAsync();
        }

        public static async Task InitializeStaffPatientAssociatios(IAPIDbContext context)
        {
            if (!await context.CanConnectAsync() || context.StaffPatientAssociations.Any())
                return;

            var patients = await context.Patients.ToListAsync();
            var staffs = await context.Staffs.ToListAsync();
            await context.StaffPatientAssociations.AddRangeAsync(
                new StaffPatientAssociations
                {
                    AssociationId = Guid.NewGuid(),
                    Patient = patients[0],
                    Staff = staffs[0],
                    AssociationStartDate = DbInitialize.GetDateTime(2024, 11, 12, 10, 30, 10),
                    AssociationEndDate = DbInitialize.GetDateTime(2024, 11, 12, 12, 30, 20),
                    AssociationDetails = "some details one"
                },
                new StaffPatientAssociations
                {
                    AssociationId = Guid.NewGuid(),
                    Patient = patients[1],
                    Staff = staffs[1],
                    AssociationStartDate = DbInitialize.GetDateTime(2024, 11, 13, 9, 10, 50),
                    AssociationEndDate = DbInitialize.GetDateTime(2024, 11, 13, 10, 30, 20),
                    AssociationDetails = "some details two"
                },
                new StaffPatientAssociations
                {
                    AssociationId = Guid.NewGuid(),
                    Patient = patients[2],
                    Staff = staffs[2],
                    AssociationStartDate = DbInitialize.GetDateTime(2024, 11, 10, 14, 10, 10),
                    AssociationEndDate = DbInitialize.GetDateTime(2024, 11, 10, 17, 30, 20),
                    AssociationDetails = "some details three"
                },
                new StaffPatientAssociations
                {
                    AssociationId = Guid.NewGuid(),
                    Patient = patients[3],
                    Staff = staffs[3],
                    AssociationStartDate = DbInitialize.GetDateTime(2024, 11, 14, 17, 0, 5),
                    AssociationEndDate = DbInitialize.GetDateTime(2024, 11, 14, 20, 30, 35),
                    AssociationDetails = "some details four"
                },
                new StaffPatientAssociations
                {
                    AssociationId = Guid.NewGuid(),
                    Patient = patients[4],
                    Staff = staffs[4],
                    AssociationStartDate = DbInitialize.GetDateTime(2024, 11, 15, 11, 30, 10),
                    AssociationEndDate = DbInitialize.GetDateTime(2024, 11, 15, 13, 30, 20),
                    AssociationDetails = "some details five"
                },
                new StaffPatientAssociations
                {
                    AssociationId = Guid.NewGuid(),
                    Patient = patients[5],
                    Staff = staffs[5],
                    AssociationStartDate = DbInitialize.GetDateTime(2024, 11, 16, 13, 0, 0),
                    AssociationEndDate = DbInitialize.GetDateTime(2024, 11, 16, 14, 0, 0),
                    AssociationDetails = "some details six"
                }
            );
            await context.SaveChangesAsync();
        }

        public static async Task InitializeRecordComponents(IAPIDbContext context)
        {
            if (!await context.CanConnectAsync() || context.RecordComponents.Any())
                return;

            await context.RecordComponents.AddRangeAsync(
                new RecordComponents
                {
                    ComponentCode = Guid.NewGuid(),
                    ComponentDescription = "Admission"
                },
                new RecordComponents
                {
                    ComponentCode = Guid.NewGuid(),
                    ComponentDescription = "Diagnosis"
                },
                new RecordComponents
                {
                    ComponentCode = Guid.NewGuid(),
                    ComponentDescription = "Medication"
                }
            );
            await context.SaveChangesAsync();
        }

        public static async Task InitializePatientRecords(IAPIDbContext context)
        {
            if (!await context.CanConnectAsync() || context.PatientRecords.Any())
                return;

            var patients = await context.Patients.ToListAsync();
            var components = await context.RecordComponents.ToListAsync();
            var staffs = await context.Staffs.ToListAsync();
            await context.PatientRecords.AddRangeAsync(
                new PatientRecords
                {
                    PatientRecordId = Guid.NewGuid(),
                    Patient = patients[0],
                    RecordComponent = components[0],
                    Staff = staffs[0],
                    UpdatedDate = DbInitialize.GetDateTime(2024, 11, 11, 12, 12, 0),
                    PatientRecordComponentDetails = components[0].ComponentDescription
                },
                new PatientRecords
                {
                    PatientRecordId = Guid.NewGuid(),
                    Patient = patients[1],
                    RecordComponent = components[1],
                    Staff = staffs[1],
                    UpdatedDate = DbInitialize.GetDateTime(2024, 11, 12, 10, 31, 20),
                    PatientRecordComponentDetails = components[1].ComponentDescription
                },
                new PatientRecords
                {
                    PatientRecordId = Guid.NewGuid(),
                    Patient = patients[2],
                    RecordComponent = components[2],
                    Staff = staffs[2],
                    UpdatedDate = DbInitialize.GetDateTime(2024, 11, 13, 9, 22, 45),
                    PatientRecordComponentDetails = components[2].ComponentDescription
                },
                new PatientRecords
                {
                    PatientRecordId = Guid.NewGuid(),
                    Patient = patients[3],
                    RecordComponent = components[0],
                    Staff = staffs[3],
                    UpdatedDate = DbInitialize.GetDateTime(2024, 11, 14, 16, 56, 41),
                    PatientRecordComponentDetails = components[0].ComponentDescription
                },
                new PatientRecords
                {
                    PatientRecordId = Guid.NewGuid(),
                    Patient = patients[4],
                    RecordComponent = components[1],
                    Staff = staffs[4],
                    UpdatedDate = DbInitialize.GetDateTime(2024, 11, 15, 11, 15, 9),
                    PatientRecordComponentDetails = components[1].ComponentDescription
                },
                new PatientRecords
                {
                    PatientRecordId = Guid.NewGuid(),
                    Patient = patients[5],
                    RecordComponent = components[2],
                    Staff = staffs[5],
                    UpdatedDate = DbInitialize.GetDateTime(2024, 11, 16, 4, 39, 58),
                    PatientRecordComponentDetails = components[2].ComponentDescription
                }
            );
            await context.SaveChangesAsync();
        }

        public static async Task InitializeAppointmentStatusCodes(IAPIDbContext context)
        {
            if (!await context.CanConnectAsync() || context.AppointmentStatusCodes.Any())
                return;

            await context.AppointmentStatusCodes.AddRangeAsync(
                new AppointmentStatusCodes
                {
                    AppointmentStatusCode = Guid.NewGuid(),
                    AppointmentStatusDescription = "Cancelled"
                },
                new AppointmentStatusCodes
                {
                    AppointmentStatusCode = Guid.NewGuid(),
                    AppointmentStatusDescription = "Kept"
                }
            );
            await context.SaveChangesAsync();
        }

        public static async Task InitializeAppointments(IAPIDbContext context)
        {
            if (!await context.CanConnectAsync() || context.Appointments.Any())
                return;

            var appointment_status_codes = await context.AppointmentStatusCodes.ToListAsync();
            var patients = await context.Patients.ToListAsync();
            var staffs = await context.Staffs.ToListAsync();
            await context.Appointments.AddRangeAsync(
                new Appointments
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentStatusCodes = appointment_status_codes[0],
                    Patient = patients[0],
                    Staff = staffs[0],
                    AppointmentStartDatetime = DbInitialize.GetDateTime(2024, 11, 10, 12, 13, 55),
                    AppointmentEndDatetime = DbInitialize.GetDateTime(2024, 11, 10, 14, 14, 23),
                    AppointmentDetails = "This is an appointment number one"
                },
                new Appointments
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentStatusCodes = appointment_status_codes[1],
                    Patient = patients[1],
                    Staff = staffs[1],
                    AppointmentStartDatetime = DbInitialize.GetDateTime(2024, 11, 11, 13, 43, 5),
                    AppointmentEndDatetime = DbInitialize.GetDateTime(2024, 11, 11, 15, 10, 3),
                    AppointmentDetails = "This is an appointment number rwo"
                },
                new Appointments
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentStatusCodes = appointment_status_codes[0],
                    Patient = patients[2],
                    Staff = staffs[2],
                    AppointmentStartDatetime = DbInitialize.GetDateTime(2024, 11, 12, 9, 12, 6),
                    AppointmentEndDatetime = DbInitialize.GetDateTime(2024, 11, 12, 10, 30, 0),
                    AppointmentDetails = "This is an appointment number three"
                },
                new Appointments
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentStatusCodes = appointment_status_codes[1],
                    Patient = patients[3],
                    Staff = staffs[3],
                    AppointmentStartDatetime = DbInitialize.GetDateTime(2024, 11, 13, 10, 10, 0),
                    AppointmentEndDatetime = DbInitialize.GetDateTime(2024, 11, 13, 10, 40, 13),
                    AppointmentDetails = "This is an appointment number four"
                },
                new Appointments
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentStatusCodes = appointment_status_codes[0],
                    Patient = patients[4],
                    Staff = staffs[4],
                    AppointmentStartDatetime = DbInitialize.GetDateTime(2024, 11, 14, 17, 30, 55),
                    AppointmentEndDatetime = DbInitialize.GetDateTime(2024, 11, 14, 18, 30, 23),
                    AppointmentDetails = "This is an appointment number five"
                },
                new Appointments
                {
                    AppointmentId = Guid.NewGuid(),
                    AppointmentStatusCodes = appointment_status_codes[1],
                    Patient = patients[5],
                    Staff = staffs[5],
                    AppointmentStartDatetime = DbInitialize.GetDateTime(2024, 11, 15, 10, 45, 15),
                    AppointmentEndDatetime = DbInitialize.GetDateTime(2024, 11, 15, 11, 25, 45),
                    AppointmentDetails = "This is an appointment number six"
                }
            );
            await context.SaveChangesAsync();
        }
    }
}
