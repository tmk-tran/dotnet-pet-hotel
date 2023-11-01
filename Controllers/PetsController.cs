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

  [HttpGet("{BreadId}")]
  public IActionResult GetBreadById(int BreadId)
  {
    Bread Bread = _c.Pets.Include(Bread => Bread.BakedBy).FirstOrDefault(Bread => Bread.Id == BreadId);

    if (Bread is null)
    {
      return NotFound();
    }

    return Ok(Bread);
  }

  [HttpPost]
  public ActionResult AddBread(Bread Bread)
  {
    Baker Baker = _c.Bakers.Find(Bread.BakedById); 

    if (Baker is null)
    {
      return NotFound();
    }

    _c.Breads.Add(Bread);
    _c.SaveChanges();

    return CreatedAtAction(nameof(GetBreadById), new { Id = Bread.Id }, Bread);
  }

  [HttpDelete("{BreadId}")]
  public IActionResult DeleteBread(int BreadId)
  {
    Bread Bread = _c.Breads.Find(BreadId);

    if (Bread is null)
    {
      return NotFound();
    }

    _c.Breads.Remove(Bread);
    _c.SaveChanges();

    return NoContent();
  }

  [HttpPut("bake/{BreadId}")]
  public IActionResult BakeBread(int BreadId)
  {
    Bread Bread = _c.Breads.Find(BreadId);

    if (Bread is null)
    {
      return NotFound();
    }

    Bread.Bake();

    _c.Breads.Update(Bread);
    _c.SaveChanges();

    return NoContent();
  }

  [HttpPut("sell/{BreadId}")]
  public IActionResult SellBread(int BreadId)
  {
    Bread Bread = _c.Breads.Find(BreadId);

    if (Bread is null)
    {
      return NotFound();
    }

    Bread.Sell();

    _c.Breads.Update(Bread);
    _c.SaveChanges();

    return NoContent();
  }

    [HttpPut("{BreadId}")]
  public IActionResult UpdateBread(int BreadId, Bread Bread)
  {
    if (BreadId != Bread.Id)
    {
      return BadRequest();
    }

    bool ExistingBread = _c.Breads.Any(Bread => Bread.Id == BreadId);

    if (ExistingBread is false)
    {
      return NotFound();
    }

    _c.Breads.Update(Bread);
    _c.SaveChanges();

    return NoContent();
  }
}