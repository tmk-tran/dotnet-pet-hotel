using System.ComponentModel.DataAnnotations;
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
  public PetBreed PetBreed { get; set; }
  [Required]
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public PetColor PetColor { get; set; }

  public PetOwner OwnedBy { get; set; }
  public int OwnedById { get; set; }

}
