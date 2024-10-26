using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnownForTitleController : ControllerBase
    {
        private readonly IDataService _dataService;

        public KnownForTitleController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/KnownForTitle/{nconst}
        [HttpGet("{nconst}")]
        public ActionResult<IList<KnownForTitle>> GetKnownForTitlesByPerson(string nconst)
        {
            return Ok(_dataService.GetKnownForTitlesByPerson(nconst));
        }

        // POST: api/KnownForTitle
        //[HttpPost]
        //public ActionResult<KnownForTitle> AddKnownForTitle([FromBody] KnownForTitle knownForTitle)
        //{
        //    var result = _dataService.AddKnownForTitle(knownForTitle.NConst, knownForTitle.KnownForTitles);
        //    return CreatedAtAction(nameof(GetKnownForTitlesByPerson), new { nconst = knownForTitle.NConst }, result);
        //}

        //// DELETE: api/KnownForTitle/{nconst}/{knownForTitle}
        //[HttpDelete("{nconst}/{knownForTitle}")]
        //public IActionResult DeleteKnownForTitle(string nconst, string knownForTitle)
        //{
        //    _dataService.DeleteKnownForTitle(nconst, knownForTitle);
        //    return NoContent();
        //}
    }
}
