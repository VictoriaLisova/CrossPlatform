namespace Lab5.Models.APILab6Models
{
    public class StaffPatientAssociations
    {
        public Guid AssociationId { get; set; }
        public DateTimeOffset AssociationStartDate { get; set; }
        public DateTimeOffset AssociationEndDate { get; set; }
        public string? AssociationDetails { get; set; }
        public Guid PatientId { get; set; }
        public Guid StaffId { get; set; }
        public Patients? Patient { get; set; }
        public Staff? Staff { get; set; }   
    }
}
