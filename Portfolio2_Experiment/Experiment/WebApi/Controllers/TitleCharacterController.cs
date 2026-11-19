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
    public class TitleCharacterController : ControllerBase
    {
        private readonly IDataService _dataService;

        public TitleCharacterController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/TitleCharacter/{nconst}
        [HttpGet("{nconst}")]
        public ActionResult<IList<TitleCharacterDto>> GetTitleCharactersByPerson(string nconst)
        {
            var titleCharacters = _dataService.GetTitleCharactersByPerson(nconst);
            if (titleCharacters == null || !titleCharacters.Any())
            {
                return NotFound();
            }

            var dtoList = titleCharacters.Select(tc => new TitleCharacterDto
            {
                NConst = tc.NConst,
                TConst = tc.TConst,
                Character = tc.Character,
                Ordering = tc.Ordering
            }).ToList();

            return Ok(dtoList);
        }
    }
}
