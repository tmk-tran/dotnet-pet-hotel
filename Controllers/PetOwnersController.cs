using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pet_hotel.Models;

namespace pet_hotel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetOwnersController : ControllerBase
{
    private readonly ApplicationContext _c;
    public PetOwnersController(ApplicationContext c)
    {
        _c = c;
    }
// GET for all Owners
    [HttpGet]
    public ActionResult GetPetOwners()
    {
        List<PetOwner> PetOwners = _c.PetOwners.Include(PetOwner => PetOwner.Pets).ToList();

        return Ok(PetOwners);
    }
// GET for a spicific OWNER
    [HttpGet("{PetOwnerId}")]
    public IActionResult GetPetOwnerById(int PetOwnerId)
    {
        PetOwner PetOwner = _c.PetOwners.Find(PetOwnerId);
        if (PetOwner == null)
        {
            return NotFound();
        }
        return Ok(PetOwner);
    }
// POST for adding new owner
    [HttpPost]
    public ActionResult AddPetOwner(PetOwner PetOwner)
    {
        _c.PetOwners.Add(PetOwner);
        _c.SaveChanges();
        return CreatedAtAction(nameof(GetPetOwnerById), new { Id = PetOwner.Id }, PetOwner);
    }
// PUT for updating count
    [HttpPut("{PetOwnerId}")]
    public IActionResult UpdatePetOwner(int PetOwnerId, PetOwner PetOwner)
    {
        if (PetOwnerId != PetOwner.Id)
        {
            return BadRequest();
        }
        bool ExistingPetOwner = _c.PetOwners.Any(PetOwner => PetOwner.Id == PetOwnerId);
        if (ExistingPetOwner is false)
        {
            return NotFound();
        }
        _c.PetOwners.Update(PetOwner);
        _c.SaveChanges();
        return NoContent();
    }

// DELETE OWNER
    [HttpDelete("{PetOwnerId}")]
    public ActionResult DeletePetOwner(int PetOwnerId)
    {
        PetOwner PetOwner = _c.PetOwners.Find(PetOwnerId);
        if (PetOwner == null)
        {
            return NotFound();
        }
        _c.PetOwners.Remove(PetOwner);
        _c.SaveChanges();
        return NoContent();
    }
};