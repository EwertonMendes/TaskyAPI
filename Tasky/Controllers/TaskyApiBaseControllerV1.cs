using Microsoft.AspNetCore.Mvc;

namespace Tasky.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public abstract class TaskyApiBaseControllerV1 : ControllerBase
    {
    }
}
