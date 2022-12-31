using Microsoft.AspNetCore.Mvc;

namespace Tasky.Controllers.BaseControllers;

[Route("api/v1/[controller]/{userId}/[action]")]
public abstract class AuthorizedBaseControllerV1 : ControllerBase
{
}
