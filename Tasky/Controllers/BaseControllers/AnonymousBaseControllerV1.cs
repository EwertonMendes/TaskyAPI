using Microsoft.AspNetCore.Mvc;

namespace Tasky.Controllers.BaseControllers;

[Route("api/v1/[controller]/[action]")]
public abstract class AnonymousBaseControllerV1 : ControllerBase
{
}
