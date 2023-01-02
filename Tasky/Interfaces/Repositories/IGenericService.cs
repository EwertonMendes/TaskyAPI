namespace Tasky.Interfaces.Repositories;

public interface IGenericService
{
    Task ValidateUserId(int userId);
}
