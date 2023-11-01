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
    List<Pet> Pets = _c.Pets.Include(pet => pet.OwnedBy).ToList();

    return Ok(Pets);
  }

  [HttpGet("{PetId}")]
  public IActionResult GetPetById(int PetId)
  {
    Pet Pet = _c.Pets.Include(Pet => Pet.OwnedBy).FirstOrDefault(Pet => Pet.Id == PetId);

    if (Pet is null)
    {
      return NotFound();
    }

    return Ok(Pet);
  }

//   [HttpPost]
//   public ActionResult AddPet(Pet Pet)
//   {
//     PetOwner PetOwner = _c.PetOwners.Find(Pet.OwnedById); 

//     if (PetOwner is null)
//     {
//       return NotFound();
//     }

//     _c.Pets.Add(Pet);
//     _c.SaveChanges();

//     return CreatedAtAction(nameof(GetPetById), new { Id = Pet.Id }, Pet);
//   }

//   [HttpDelete("{PetId}")]
//   public IActionResult DeletePet(int PetId)
//   {
//     Pet Pet = _c.Pets.Find(PetId);

//     if (Pet is null)
//     {
//       return NotFound();
//     }

//     _c.Pets.Remove(Pet);
//     _c.SaveChanges();

//     return NoContent();
//   }

//   [HttpPut("bake/{PetId}")]
//   public IActionResult BakeBread(int PetId)
//   {
//     Bread Bread = _c.Breads.Find(BreadId);

//     if (Bread is null)
//     {
//       return NotFound();
//     }

//     Bread.Bake();

//     _c.Breads.Update(Bread);
//     _c.SaveChanges();

//     return NoContent();
//   }
// // I'll start here
//   [HttpPut("pets/{BreadId}")]
//   public IActionResult SellBread(int BreadId)
//   {
//     Bread Bread = _c.Breads.Find(BreadId);

//     if (Bread is null)
//     {
//       return NotFound();
//     }

//     Bread.Sell();

//     _c.Breads.Update(Bread);
//     _c.SaveChanges();

//     return NoContent();
//   }

//     [HttpPut("{BreadId}")]
//   public IActionResult UpdateBread(int BreadId, Bread Bread)
//   {
//     if (BreadId != Bread.Id)
//     {
//       return BadRequest();
//     }

//     bool ExistingBread = _c.Breads.Any(Bread => Bread.Id == BreadId);

//     if (ExistingBread is false)
//     {
//       return NotFound();
//     }

//     _c.Breads.Update(Bread);
//     _c.SaveChanges();

//     return NoContent();
//   }
}