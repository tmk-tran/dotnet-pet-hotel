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
// GET all 
  [HttpGet]
  public ActionResult GetPets()
  {
    List<Pet> Pets = _c.Pets.Include(pet => pet.PetOwner).ToList();

    return Ok(Pets);
  }
// Get by Pet ID
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
// Post for new pet
  [HttpPost]
  public ActionResult AddPet(Pet Pet)
  {
    PetOwner PetOwner = _c.PetOwners.Find(Pet.PetOwnerId);

    if (PetOwner is null)
    {
      return NotFound();
    }

    // _c.PetOwner.Pets.Add(Pet); 

    // string iso8601Timestamp = Pet.CheckedInAt?.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ");

    Pet.CheckedInAt = "0001-01-01T00:00:00";

    _c.Pets.Add(Pet);
    _c.SaveChanges();

    return CreatedAtAction(nameof(GetPetById), new { Id = Pet.Id }, Pet);
  }

// DELETE
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

    return Ok();
  }
// PUT for CHECK IN
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
// PUT for CHECK OUT
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
// PUT  
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

    return Ok();
  }
}