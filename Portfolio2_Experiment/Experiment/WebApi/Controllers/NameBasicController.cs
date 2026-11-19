using DataLayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DTOs;

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
        public ActionResult<NameBasicDetailDto> GetNameBasicByNConst(string nconst)
        {
            var person = _dataService.GetPersonByNConst(nconst);
            if (person == null)
            {
                return NotFound();
            }

            // Populate the DTO, splitting KnownForTitles if it's a comma-separated string
            var dto = new NameBasicDetailDto
            {
                NConst = person.NConst,
                PrimaryName = person.ActualName,
                BirthYear = person.BirthYear,
                DeathYear = person.DeathYear,
                PrimaryProfession = person.PrimaryProfession,
                KnownForTitles = person.KnownForTitles?.Split(',').ToList() ?? new List<string>()
            };

            return Ok(dto);
        }

        // GET: api/NameBasic
        [HttpGet]
        public ActionResult<IList<NameBasicDetailDto>> GetAllNameBasics()
        {
            var persons = _dataService.GetAllPersons();
            var dtoList = persons.Select(p => new NameBasicDetailDto
            {
                NConst = p.NConst,
                PrimaryName = p.ActualName,
                BirthYear = p.BirthYear,
                DeathYear = p.DeathYear,
                PrimaryProfession = p.PrimaryProfession,
                KnownForTitles = p.KnownForTitles?.Split(',').ToList() ?? new List<string>()
            }).ToList();

            return Ok(dtoList);
        }
    }
}
