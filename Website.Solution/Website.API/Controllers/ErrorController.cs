using Microsoft.AspNetCore.Mvc;
using Website.API.Errors;

namespace Website.API.Controllers
{
	[Route("error/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ErrorController : ControllerBase
	{
		public ActionResult Error(int code)
		{
			return NotFound(new BaseErrorResponse(code, "Not Found End Point"));
		}
	}
}
