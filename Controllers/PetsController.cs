using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pet_hotel.Models;

namespace pet_hotel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
  private readonly ApplicationContext _c;
  public PetsController(ApplicationContext c)
  {
    _c = c;
  }

  [HttpGet]
  public ActionResult GetPets()
  {
    List<Pet> Pets = _c.Pets.Include(Pet => Pet.PetOwner).ToList();

    return Ok(Pets);
  }

  [HttpGet("{PetId}")]
  public IActionResult GetPetById(int PetId)
  {
    Pet Pet = _c.Pets.Include(Pet => Pet.PetOwner).FirstOrDefault(Pet => Pet.Id == PetId);

    if (Pet is null)
    {
      return NotFound();
    }

    return Ok(Pet);
  }

  [HttpPost]
  public ActionResult AddPet(Pet Pet)
  {
    PetOwner PetOwner = _c.PetOwners.Find(Pet.PetOwnerId);

    if (PetOwner is null)
    {
      return NotFound();
    }

    // _c.PetOwner.Pets.Add(Pet); 

    string iso8601Timestamp = Pet.CheckedInAt?.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");

    // Pet.CheckedInAt = DateTime.UtcNow;

    _c.Pets.Add(Pet);
    _c.SaveChanges();

    return CreatedAtAction(nameof(GetPetById), new { Id = Pet.Id }, Pet);
  }

  [HttpDelete("{PetId}")]
  public IActionResult DeletePet(int PetId)
  {
    Pet Pet = _c.Pets.Find(PetId);

    if (Pet is null)
    {
      return NotFound();
    }

    _c.Pets.Remove(Pet);
    _c.SaveChanges();

    return NoContent();
  }

  [HttpPut("{PetId}/checkin")]
  public IActionResult CheckInPet(int PetId)
  {
    Pet Pet = _c.Pets.Find(PetId);

    if (Pet is null)
    {
      return NotFound();
    }

    Pet.CheckIn();

    _c.Pets.Update(Pet);
    _c.SaveChanges();

    return NoContent();
  }

  [HttpPut("{PetId}/checkout")]
  public IActionResult CheckOutPet(int PetId)
  {
    Pet Pet = _c.Pets.Find(PetId);

    if (Pet is null)
    {
      return NotFound();
    }

    Pet.CheckOut();

    _c.Pets.Update(Pet);
    _c.SaveChanges();

    return NoContent();
  }

  [HttpPut("{PetId}")]
  public IActionResult UpdatePet(int PetId, Pet Pet)
  {
    if (PetId != Pet.Id)
    {
      return BadRequest();
    }

    bool ExistingPet = _c.Pets.Any(Pet => Pet.Id == PetId);

    if (ExistingPet is false)
    {
      return NotFound();
    }

    _c.Pets.Update(Pet);
    _c.SaveChanges();

    return NoContent();
  }
}