using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureSample.Application.Dto.Request;

public record CreateOrUpdateCountryRequest
(
    [Required]
    [MaxLength(100)]
    string Name,

    [MaxLength(2)]
    string? Code
);
