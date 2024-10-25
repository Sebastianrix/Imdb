using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitlePrincipalController : ControllerBase
    {
        private readonly IDataService _dataService;

        public TitlePrincipalController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/TitlePrincipal/{tconst}
        [HttpGet("{tconst}")]
        public ActionResult<IList<TitlePrincipal>> GetTitlePrincipalsByTitle(string tconst)
        {
            return Ok(_dataService.GetTitlePrincipalsByTitle(tconst));
        }

        // POST: api/TitlePrincipal
        [HttpPost]
        public ActionResult<TitlePrincipal> AddTitlePrincipal([FromBody] TitlePrincipal titlePrincipal)
        {
            var result = _dataService.AddTitlePrincipal(
                titlePrincipal.TConst,
                titlePrincipal.NConst,
                titlePrincipal.Ordering,
                titlePrincipal.Category,
                titlePrincipal.Job);

            return CreatedAtAction(nameof(GetTitlePrincipalsByTitle), new { tconst = titlePrincipal.TConst }, result);
        }

        // DELETE: api/TitlePrincipal/{tconst}/{nconst}/{ordering}
        [HttpDelete("{tconst}/{nconst}/{ordering}")]
        public IActionResult DeleteTitlePrincipal(string tconst, string nconst, int ordering)
        {
            _dataService.DeleteTitlePrincipal(tconst, nconst, ordering);
            return NoContent();
        }
    }
}
