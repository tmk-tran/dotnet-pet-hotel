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
    List<Pet> Pets = _c.Pets.Include(p => p.PetOwner).ToList();

    return Ok(Pets);
  }

  [HttpGet("{id}")]
  public IActionResult GetPetById(int id)
  {
    Pet Pet = _c.Pets.Include(p => p.PetOwner).SingleOrDefault(p => p.Id == id);

    if (Pet is null)
    {
      return NotFound();
    }

    return Ok(Pet);
  }

  [HttpPost]
  public ActionResult AddPet(Pet pet)
  {
    _c.Pets.Add(pet);
    _c.SaveChanges();

    Pet CreatedPet = _c.Pets.OrderByDescending(p => p.Id).Include(p => p.PetOwner).FirstOrDefault();
    CreatedPet.CheckedInAt = DateTime.MinValue;
    return CreatedAtAction(nameof(GetPetById), new { Id = pet.Id }, CreatedPet);
  }

  [HttpPut("{id}")]
  public IActionResult UpdatePet(Pet pet, int id)
  {
    if (pet.Id != id)
    {
      return BadRequest();
    }

    bool ExistingPet = _c.Pets.Any(p => p.Id == id);

    if (ExistingPet is false)
    {
      return NotFound();
    }

    _c.Pets.Update(pet);
    _c.SaveChanges();

    Pet Pet = _c.Pets.Find(id);

    return Ok(Pet);
  }

  [HttpPut("{id}/checkin")]
  public IActionResult CheckInPet(int id)
  {
    Pet Pet = _c.Pets.Find(id);

    if (Pet is null)
    {
      return NotFound();
    }

    Pet.CheckedInAt = DateTime.UtcNow;

    _c.Pets.Update(Pet);
    _c.SaveChanges();

    return Ok(Pet);
  }

  [HttpPut("{id}/checkout")]
  public IActionResult CheckOutPet(int id)
  {
    Pet Pet = _c.Pets.Find(id);

    if (Pet is null)
    {
      return NotFound();
    }

    Pet.CheckedInAt = DateTime.Parse("0001-01-01T00:00:00");

    _c.Pets.Update(Pet);
    _c.SaveChanges();

    return Ok();
  }

  [HttpDelete("{id}")]
  public IActionResult DeletePet(int id)
  {
    Pet Pet = _c.Pets.Find(id);

    if (Pet is null)
    {
      return NotFound();
    }

    _c.Pets.Remove(Pet);
    _c.SaveChanges();

    return NoContent();
  }
}