using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ooapi.v5.core.Repositories;
using ooapi.v5.Models.Params;

namespace ooapi.v5.Controllers
{
    public class HealthController : ControllerBase
    {

        //public HealthController(IConfiguration configuration, CoreDBContext dbContext) : base(configuration, dbContext)
        //{

        //}

        public HealthController(IConfiguration configuration, CoreDBContext dbContext) 
        {
            var i = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="primaryCodeParam"></param>
        /// <param name="filterParams"></param>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("health")]
        public virtual IActionResult GetHealth([FromQuery] PrimaryCodeParam primaryCodeParam, [FromQuery] FilterParams filterParams, [FromQuery] PagingParams pagingParams)
        {
            var dit = primaryCodeParam;
            var dat = filterParams;
            var dut = pagingParams;
            return Ok("gezond");
        }
    }
}
