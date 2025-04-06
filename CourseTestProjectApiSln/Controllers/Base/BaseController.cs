using CourseTestProjectApiSln.Business.DTOs.BaseResponseModel;
using Microsoft.AspNetCore.Mvc;

namespace CourseTestProjectApiSln.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        protected ActionResult CreateResponse<T>(GenericResponseModel<T> response)
        {
            return StatusCode(response.StatusCode, response);
        }
    }
}
