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

    [HttpGet]
    public ActionResult GetPetOwners()
    {
        List<PetOwner> PetOwners = _c.PetOwners.ToList();

        return Ok(PetOwners);
    }

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
}

// [HttpPost]
// public ActionResult AddPetOwners(PetOwners PetOwners)
// {
//     _c.PetOwnerss.Add(PetOwners);
//     _c.SaveChanges();
//     return CreatedAtAction(nameof(GetPetOwnersById), new { Id = PetOwners.Id }, PetOwners);
// }