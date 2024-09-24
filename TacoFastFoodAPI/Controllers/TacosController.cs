using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetAll(bool? softShell = null)
        {
            List<Taco> result = dbContext.Tacos.ToList();

            if(softShell != null)
            {
                result = result.Where(x => x.SoftShell == softShell).ToList();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Taco result = dbContext.Tacos.FirstOrDefault(x => x.Id == id);
            if(result == null)
            {
                return NotFound("Could not find taco.");
            }
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult AddTaco([FromBody]Taco newTaco)
        {
            newTaco.Id = 0;
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();

            return Created($"/api/Taco/{newTaco.Id}", newTaco);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            Taco t = dbContext.Tacos.FirstOrDefault(x => x.Id == id);
            if(t == null)
            {
                return NotFound("No matching id found");
            }
            else
            {
                dbContext.Tacos.Remove(t);
                dbContext.SaveChanges();
                return NoContent();
            }
        }
    }
}
