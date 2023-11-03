using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pet_hotel.Models;

namespace pet_hotel.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetOwnersController : ControllerBase
{
  private readonly ApplicationContext _c;
  public PetOwnersController(ApplicationContext c)
  {
    _c = c;
  }

  [HttpGet]
  public ActionResult GetPetOwners()
  {
    List<PetOwner> PetOwners = _c.PetOwners.Include(p => p.Pets).ToList();

    return Ok(PetOwners);
  }

  [HttpGet("{id}")]
  public IActionResult GetPetOwnerById(int id)
  {
    PetOwner PetOwner = _c.PetOwners.Find(id);

    if (PetOwner is null)
    {
      return NotFound();
    }

    return Ok(PetOwner);
  }

  [HttpPost]
  public ActionResult AddPetOwner(PetOwner petOwner)
  {
    _c.PetOwners.Add(petOwner);
    _c.SaveChanges();

    PetOwner CreatedPetOwner = _c.PetOwners.OrderByDescending(p => p.Id).Include(p => p.Pets).FirstOrDefault();

    return CreatedAtAction(nameof(GetPetOwnerById), new { Id = petOwner.Id }, CreatedPetOwner);
  }

  [HttpPut("{id}")]
  public IActionResult UpdatePetOwner(PetOwner petOwner, int id)
  {
    if (petOwner.Id != id)
    {
      return BadRequest();
    }

    bool ExistingPetOwner = _c.PetOwners.Any(p => p.Id == id);

    if (ExistingPetOwner is false)
    {
      return NotFound();
    }

    _c.PetOwners.Update(petOwner);
    _c.SaveChanges();

    PetOwner UpdatedPetOwner = _c.PetOwners.Find(id);

    return Ok(UpdatedPetOwner);
  }

  [HttpDelete("{id}")]
  public IActionResult DeletePetOwner(int id)
  {
    PetOwner PetOwner = _c.PetOwners.Find(id);

    if (PetOwner is null)
    {
      return NotFound();
    }

    _c.PetOwners.Remove(PetOwner);
    _c.SaveChanges();

    return NoContent();
  }
}