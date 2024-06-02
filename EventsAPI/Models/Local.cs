using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventsAPI.Models;

public class Local
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [JsonIgnore]
    [NotMapped]
    public Address? Address { get; set; }

    [Required(ErrorMessage = "AddressId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "AddressId must be greater than zero.")]
    public int AddressId { get; set; }

    [Required(ErrorMessage = "Latitude is required.")]
    public double Latitude { get; set; }

    [Required(ErrorMessage = "Longitude is required.")]
    public double Longitude { get; set; }

    [JsonIgnore]
    [NotMapped]
    public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public List<string>? Facilities { get; set; }

    [Required(ErrorMessage = "Capacity is required.")]
    public int Capacity { get; set; }
}
