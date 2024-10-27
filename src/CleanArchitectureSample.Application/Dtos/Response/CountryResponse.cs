using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureSample.Application.Dtos.Response;

public record CountryResponse
(
    int Id,

    [Required]
    [MaxLength(100)]
    string Name,

    [MaxLength(2)]
    string? Code
);
