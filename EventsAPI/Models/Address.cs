using EventsAPI.Enuns;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventsAPI.Models;

public class Address
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Type is required.")]
    public Types Type { get; set; }

    [Required(ErrorMessage = "Street is required.")]
    public string Street { get; set; }

    [Required(ErrorMessage = "Number is required.")]
    public string Number { get; set; }

    public string? Complement { get; set; }

    [Required(ErrorMessage = "City is required.")]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required.")]
    public string State { get; set; }

    [Required(ErrorMessage = "PostalCode is required.")]
    public string PostalCode { get; set; }

    [JsonIgnore]
    [NotMapped]
    public Local? Local { get; set; }
}
