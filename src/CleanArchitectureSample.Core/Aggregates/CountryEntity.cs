
namespace CleanArchitectureSample.Core.Aggregates;

public class CountryEntity : BaseEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string? Code { get; set; }
    public ICollection<ContactEntity> Contacts { get; set; } = default!;
}