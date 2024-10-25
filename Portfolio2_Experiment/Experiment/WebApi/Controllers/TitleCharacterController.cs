using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleCharacterController : ControllerBase
    {
        private readonly IDataService _dataService;

        public TitleCharacterController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/TitleCharacter/{nconst}
        [HttpGet("{nconst}")]
        public ActionResult<IList<TitleCharacter>> GetTitleCharactersByPerson(string nconst)
        {
            return Ok(_dataService.GetTitleCharactersByPerson(nconst));
        }

        // POST: api/TitleCharacter
        [HttpPost]
        public ActionResult<TitleCharacter> AddTitleCharacter([FromBody] TitleCharacter titleCharacter)
        {
            var result = _dataService.AddTitleCharacter(
                titleCharacter.NConst,
                titleCharacter.TConst,
                titleCharacter.Character,
                titleCharacter.Ordering);

            return CreatedAtAction(nameof(GetTitleCharactersByPerson), new { nconst = titleCharacter.NConst }, result);
        }

        // DELETE: api/TitleCharacter/{nconst}/{tconst}/{character}
        [HttpDelete("{nconst}/{tconst}/{character}")]
        public IActionResult DeleteTitleCharacter(string nconst, string tconst, string character)
        {
            _dataService.DeleteTitleCharacter(nconst, tconst, character);
            return NoContent();
        }
    }
}
