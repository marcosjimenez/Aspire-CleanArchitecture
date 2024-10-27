using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureSample.Application.Dtos.Response;

public record ContactResponse
(
    [ReadOnly(true)]
    int Id,

    [Required]
    [MaxLength(50)]
    string Name,

    [Required]
    [MaxLength(50)]
    string LastName,

    [DataType(DataType.Date)]
    DateTime? BirthDate,

    [MaxLength(50)]
    [Phone]
    string? Phone,

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    string? EMail,

    CountryResponse? Country
);
