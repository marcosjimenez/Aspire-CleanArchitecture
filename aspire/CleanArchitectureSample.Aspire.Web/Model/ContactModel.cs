namespace CleanArchitectureSample.Aspire.Web.Model
{
    public class ContactModel
    {
        public int? Id { get; set; }
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime? BirthDate { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string CountryId { get; set; } = default!;
    }
}
