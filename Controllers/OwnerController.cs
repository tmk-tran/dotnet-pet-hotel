using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pet_hotel.Models;

namespace pet_hotel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OwnerController : ControllerBase
{
    private readonly ApplicationContext _c;
    public OwnerController(ApplicationContext c)
    {
        _c = c;
    }

    [HttpGet]
    public ActionResult GetOwner()
    {
        List<Owner> Owners = _c.Owners.ToList();

        return Ok(Owners);
    }

    [HttpGet("{OwnerId}")]
    public IActionResult GetOwnerById(int OwnerId)
    {
        Owner Owner = _c.Owners.Find(OwnerId);
        if (Owner == null)
        {
            return NotFound();
        }
        return Ok(Owner);
    }
}

    // [HttpPost]
    // public IActionResult PostOwner(Owner Owner)
    // {
    //     _c.Owner.Add(Owner);
    //     _c.SaveChanges();
    //     return CreatedAtAction
    // }