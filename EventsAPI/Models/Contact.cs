using EventsAPI.Enuns;
using EventsAPI.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventsAPI.Models;

public class Contact
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    [PhoneNumber]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Type is required.")]
    public Types Type { get; set; }

    public int? LocalId { get; set; }

    public int? PersonId { get; set; }

    [JsonIgnore]
    [NotMapped]
    public Local? Local { get; set; }
}
