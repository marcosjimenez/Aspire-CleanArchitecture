namespace CleanArchitectureSample.Core.Aggregates;

public class ContactEntity : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public DateTime? BirthDate { get; set; }

    public string? Phone { get; set; }

    public string? EMail { get; set; } = default!;

    public int? CountryId { get; set; } = default!;

    public virtual CountryEntity? Country { get; set; } = default!;
}
