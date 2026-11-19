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
    public class TitlePrincipalController : ControllerBase
    {
        private readonly IDataService _dataService;

        public TitlePrincipalController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET: api/TitlePrincipal/{nconst}
        [HttpGet("{nconst}")]
        public ActionResult<IList<TitlePrincipalDto>> GetTitlePrincipalsByPerson(string nconst)
        {
            var titlePrincipals = _dataService.GetTitlePrincipalsByPerson(nconst);
            if (titlePrincipals == null || !titlePrincipals.Any())
            {
                return NotFound();
            }

            var dtoList = titlePrincipals.Select(tp => new TitlePrincipalDto
            {
                TConst = tp.TConst,
                NConst = tp.NConst,
                Ordering = tp.Ordering,
                Category = tp.Category,
                Job = tp.Job
            }).ToList();

            return Ok(dtoList);
        }
    }
}
