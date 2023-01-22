using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.Services.Dto;

public class ActorDto
{
    public int? Id { get; set; }

    [Required]
    [NameValidation(4)]
    public string Firstname { get; set; }


    [Required]
    [NameValidation(4)]
    public string Lastname { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [ActorAge(7, 99)]
    public DateTime Birthdate { get; set; }
}