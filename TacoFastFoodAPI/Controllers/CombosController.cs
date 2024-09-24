using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll()
        {
            List<Combo> result = dbContext.Combos.Include(c => c.Drink).Include(c => c.Taco).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Combo result = dbContext.Combos.Include(c => c.Drink).Include(c => c.Taco).FirstOrDefault(c => c.Id == id);
            if(result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost()]
        public IActionResult AddCombo([FromBody]Combo newCombo)
        {
            newCombo.Id = 0;
            dbContext.Combos.Add(newCombo);
            dbContext.SaveChanges();

            return Created($"/api/Combo/{newCombo.Id}", newCombo);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            Combo c = dbContext.Combos.FirstOrDefault(x => x.Id == id);
            if(c == null)
            {
                return NotFound();
            }
            else
            {
                dbContext.Combos.Remove(c);
                dbContext.SaveChanges();
                return NoContent();
            }
        }
    }
}
