using Microsoft.AspNetCore.Mvc;
using Warehouse.App;

namespace Warehouse.API.Controllers
{
    public class ApplicationController : ControllerBase
    {
        protected ActionResult HandleRequest(CommandResponse response)
        {
            if (!response.HasErrors)
            {
                return Ok();
            }
            return BadRequest(response.Errors);
        }

        protected ActionResult HandleRequest<T>(CommandResponse<T> response)
        {
            if (!response.HasErrors)
            {
                return Ok(response.Body);
            }
            return BadRequest(response.Errors);
        }
    }
}