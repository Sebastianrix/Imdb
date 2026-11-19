using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DTOs;

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
        public ActionResult<IList<KnownForTitleDto>> GetKnownForTitlesByPerson(string nconst)
        {
            var knownForTitles = _dataService.GetKnownForTitlesByPerson(nconst);
            if (knownForTitles == null || !knownForTitles.Any())
            {
                return NotFound();
            }

            var dtoList = knownForTitles.Select(k => new KnownForTitleDto
            {
                NConst = k.NConst,
                KnownForTitles = k.KnownForTitles
            }).ToList();

            return Ok(dtoList);
        }
    }
}
