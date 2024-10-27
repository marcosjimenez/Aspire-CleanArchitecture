using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureSample.Application.Dtos.Request;

public record CreateOrUpdateContactRequest
(
    [Required]
    [MaxLength(50)]
    string Name,

    [Required]
    [MaxLength(50)]
    string LastName,

    DateTime? BirthDate,

    [MaxLength(100)]
    [EmailAddress]
    string? EMail,


    [MaxLength(50)]
    [Phone]
    string? Phone,

    int? CountryId
);
