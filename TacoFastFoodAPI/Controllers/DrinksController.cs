using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll(string? sortByCost = null)
        {
            List<Drink> result = dbContext.Drinks.ToList();

            if(sortByCost != null)
            {
                if(sortByCost.ToLower() == "ascending")
                {
                    result = result.OrderBy(x => x.Cost).ToList();
                }
                else if(sortByCost.ToLower() == "descending")
                {
                    result = result.OrderByDescending(x => x.Cost).ToList();
                }
                else
                {
                    return BadRequest("Order by Ascending or Descending");
                }
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Drink result = dbContext.Drinks.FirstOrDefault(x => x.Id == id);
            if(result == null)
            {
                return BadRequest("Could not find id");
            }
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult AddDrink([FromBody]Drink newDrink)
        {
            newDrink.Id = 0;
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();

            return Created($"/api/Drink/{newDrink.Id}", newDrink);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDrink(int id, [FromBody]Drink updatedDrink)
        {
            if(id != updatedDrink.Id)
            {
                return BadRequest("Id's do not match");
            }
            if(!dbContext.Drinks.Any(x => x.Id == id))
            {
                return NotFound("No matching Id");
            }
            dbContext.Drinks.Update(updatedDrink);
            dbContext.SaveChanges();
            return Ok(updatedDrink);
        }
    }
}
