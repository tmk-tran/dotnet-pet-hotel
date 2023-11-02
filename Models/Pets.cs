using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace pet_hotel.Models;


public enum PetBreed
{
  Shepherd,
  Poodle,
  Beagle,
  Bulldog,
  Terrier,
  Boxer,
  Labrador,
  Retriever
}
public enum PetColor
{
  Black,
  White,
  Golden,
  Tricolor,
  Spotted

}

public class Pet
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public PetBreed Breed { get; set; }
  [Required]
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public PetColor Color { get; set; }
  public PetOwner PetOwner { get; set; }
  public int PetOwnerId { get; set; }

  public DateTime? CheckedInAt { get; set; }
  public void CheckIn()
  {
    CheckedInAt = DateTime.UtcNow;
  }

  public void CheckOut()
  {
    CheckedInAt = null;
  }
}
