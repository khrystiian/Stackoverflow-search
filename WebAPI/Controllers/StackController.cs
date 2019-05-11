using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;
using Serilog;
using System;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StackController : ControllerBase
    {
        private readonly IStackoverflowReader stack;

        public StackController(IStackoverflowReader _stack)
        {
            stack = _stack ?? throw new ArgumentNullException(nameof(stack));
        }

        // GET api/values/5
        [HttpGet]
        public IList<Items> Get(SearchInput userInput)
        {
            Log.Information("In the controller!");
            return stack.InputRead(userInput);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
