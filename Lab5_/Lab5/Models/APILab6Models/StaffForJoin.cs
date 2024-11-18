namespace Lab5.Models.APILab6Models
{
	public class StaffForJoin : Staff
	{
        public ICollection<string>? PatientRecords { get; set; }
    }
}
