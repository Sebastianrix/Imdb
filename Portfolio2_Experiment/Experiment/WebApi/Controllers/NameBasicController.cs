using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameBasicController : ControllerBase
    {
        private readonly IDataService _dataService;

        public NameBasicController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/NameBasic/{nconst}
        [HttpGet("{nconst}")]
        public ActionResult<Person> GetNameBasicByNConst(string nconst)
        {
            var nameBasic = _dataService.GetPersonByNConst(nconst);  
            if (nameBasic == null)
            {
                return NotFound();
            }
            return Ok(nameBasic);
        }

        // GET: api/NameBasic
        [HttpGet]
        public ActionResult<IList<Person>> GetAllNameBasics()
        {
            return Ok(_dataService.GetAllPersons());  // Assuming this retrieves all NameBasic entries
        }

      
        [HttpPost]
        public ActionResult<Person> AddNameBasic([FromBody] Person nameBasic)
        {
            var result = _dataService.AddPerson(
                nameBasic.ActualName,
                nameBasic.BirthYear,
                nameBasic.DeathYear,
                nameBasic.PrimaryProfession,
                nameBasic.KnownForTitles);

            return CreatedAtAction(nameof(GetNameBasicByNConst), new { nconst = result.NConst }, result);
        }

        // DELETE: api/NameBasic/{nconst}
        [HttpDelete("{nconst}")]
        public IActionResult DeleteNameBasic(string nconst)
        {
            _dataService.DeletePerson(nconst);  
            return NoContent();
        }
    }
}
