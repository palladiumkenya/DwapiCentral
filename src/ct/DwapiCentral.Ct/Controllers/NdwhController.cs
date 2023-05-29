using DwapiCentral.Shared.Domain.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DwapiCentral.Ct.Controllers
{
    
    [ApiController]    
    public class NdwhController : Controller
    {
        
        [HttpPost]
        [Route("api/[controller]/verify")]
        public IActionResult Verify([FromBody] Subscriber subscriber)
        {
            {
                if (null == subscriber)
                {
                    return BadRequest();
                }

                if (subscriber.Verify())
                    return Ok(new
                    {
                        registryName = "National DataWarehouse"
                    });

                return Unauthorized();
            }
        }
    }
}
