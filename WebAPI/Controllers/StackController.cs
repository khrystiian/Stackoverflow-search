using BusinessLayer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StackController : ControllerBase
    {
        private readonly IElasticsearch eSearch;

        public StackController(IElasticsearch _eSearch) => eSearch = _eSearch ?? throw new ArgumentNullException(nameof(eSearch));

        // GET api/values/5
        [EnableCors]
        [HttpPost]
        public ResultModel Post([FromBody]SearchModel userInput)
        {
            return eSearch.SearchOnELK(userInput);
        }
    
        [HttpGet]
        public string Get()
        {
            return "value1, value2";
        }
    }
}
