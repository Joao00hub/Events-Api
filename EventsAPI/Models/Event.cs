using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventsAPI.Models;

public class Event
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "LocalId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "LocalId must be greater than zero.")]
    public int LocalId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "EventInitDate is required.")]
    public DateTimeOffset EventInitDate { get; set; }

    [Required(ErrorMessage = "EventEndDate is required.")]
    public DateTimeOffset EventEndDate { get; set; }

    public int? ParticipantsLimit{ get; set; }

    [JsonIgnore]
    [NotMapped]
    public Local? Local { get; set; }
}
