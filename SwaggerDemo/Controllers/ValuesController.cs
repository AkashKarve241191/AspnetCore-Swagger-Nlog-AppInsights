using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace SwaggerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IResultProvider resultProvider;
        private readonly ILogger logger;
        public ValuesController(IResultProvider result, ILogger<ValuesController> logger)
        {
            resultProvider = result;
            this.logger = logger;
        }

        private IList<string> Values = new List<string> { "value1", "value2" };
        /// <summary>
        ///GET All Values
        /// </summary>
        [HttpGet]
        public ActionResult<IList<string>> Get()
        {
            this.logger.LogWarning("Values Controller Get() entered");
            // throw new  ArgumentException("Values Contol errorrrrrrrrrrrr");
            return Ok(this.resultProvider.ProvideResult());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            Values.Add(value);
            return Ok(Values);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
